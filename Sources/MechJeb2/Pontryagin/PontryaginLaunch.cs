using System;
using System.Collections.Generic;
using UnityEngine;

using Log = MechJeb2.Log;

namespace MuMech {
    public class PontryaginLaunch : PontryaginBase {
        public PontryaginLaunch(MechJebCore core, double mu, Vector3d r0, Vector3d v0, Vector3d pv0, Vector3d pr0, double dV) : base(core, mu, r0, v0, pv0, pr0, dV)
        {
        }

        public bool omitCoast;

        double rTm;
        double vTm;
        double gamma;
        double inc;
        double smaT;
        Vector3d hT;

        // 5-constraint PEG with fixed LAN
        public void flightangle5constraint(double rTm, double vTm, double gamma, Vector3d hT)
        {
            QuaternionD rot = Quaternion.Inverse(Planetarium.fetch.rotation);
            this.rTm = rTm / r_scale;
            this.vTm = vTm / v_scale;
            this.gamma = gamma;
            this.hT = rot * hT / r_scale / v_scale;
            bcfun = flightangle5constraint;
        }

        private void flightangle5constraint(double[] yT, double[] z, bool terminal)
        {
            Vector3d rf = new Vector3d(yT[0], yT[1], yT[2]);
            Vector3d vf = new Vector3d(yT[3], yT[4], yT[5]);
            Vector3d pvf = new Vector3d(yT[6], yT[7], yT[8]);
            Vector3d prf = new Vector3d(yT[9], yT[10], yT[11]);
            Vector3d hf = Vector3d.Cross(rf, vf);

            Vector3d hmiss = hf - hT;

            // 5 constraints
            if (!terminal)
            {
                z[0] = ( rf.magnitude * rf.magnitude - rTm * rTm ) / 2.0;
                z[1] = Vector3d.Dot(rf, vf) - rf.magnitude * vf.magnitude * Math.Sin(gamma);
                z[2] = hmiss[0];
                z[3] = hmiss[1];
                z[4] = hmiss[2];

                // transversality - free argp
                z[5] = Vector3d.Dot(Vector3d.Cross(prf, rf) + Vector3d.Cross(pvf, vf), hT);
            }
            else
            {
                z[0] = hmiss.magnitude;
                z[1] = z[2] = z[3] = z[4] = z[5] = 0.0;
            }
        }

        // 4-constraint PEG with free LAN
        public void flightangle4constraint(double rTm, double vTm, double gamma, double inc)
        {
            Log.dbg("call stack: {0}", Environment.StackTrace);
            this.rTm = rTm / r_scale;
            this.vTm = vTm / v_scale;
            Log.dbg("4constraint vTm = {0} v_scale = {1} vTm_bar = {2}", vTm, v_scale, this.vTm );
            Log.dbg("4constraint rTm = {0} r_scale = {1} rTm_bar = {2}", rTm, r_scale, this.rTm );
            this.gamma = gamma;
            this.inc = inc;
            bcfun = flightangle4constraint;
        }

        private void flightangle4constraint(double[] yT, double[] z, bool terminal)
        {
            Vector3d rf = new Vector3d(yT[0], yT[1], yT[2]);
            Vector3d vf = new Vector3d(yT[3], yT[4], yT[5]);
            Vector3d pvf = new Vector3d(yT[6], yT[7], yT[8]);
            Vector3d prf = new Vector3d(yT[9], yT[10], yT[11]);

            Vector3d n = new Vector3d(0, -1, 0);  /* angular momentum vectors point south in KSP and we're in xzy coords */
            Vector3d rn = Vector3d.Cross(rf, n);
            Vector3d vn = Vector3d.Cross(vf, n);
            Vector3d hf = Vector3d.Cross(rf, vf);

            if (!terminal)
            {
                z[0] = ( rf.magnitude * rf.magnitude - rTm * rTm ) / 2.0;
                z[1] = ( vf.magnitude * vf.magnitude - vTm * vTm ) / 2.0;
                z[2] = Vector3d.Dot(n, hf) - hf.magnitude * Math.Cos(inc);
                z[3] = Vector3d.Dot(rf, vf) - rf.magnitude * vf.magnitude * Math.Sin(gamma);
                z[4] = rTm * rTm * ( Vector3d.Dot(vf, prf) - vTm * Math.Sin(gamma) / rTm * Vector3d.Dot(rf, prf) ) -
                    vTm * vTm * ( Vector3d.Dot(rf, pvf) - rTm * Math.Sin(gamma) / vTm * Vector3d.Dot(vf, pvf) );
                z[5] = Vector3d.Dot(hf, prf) * Vector3d.Dot(hf, rn) + Vector3d.Dot(hf, pvf) * Vector3d.Dot(hf, vn);
            }
            else
            {
                double hTm = rTm * vTm * Math.Cos(gamma);

                z[0] = hf.magnitude - hTm;
                z[1] = z[2] = z[3] = z[4] = z[5] = 0.0;
            }
        }

        public override void Bootstrap(double t0)
        {
            // build arcs off of ksp stages, with coasts
            List<Arc> arcs = new List<Arc>();
            for(int i = 0; i < stages.Count; i++)
            {
                //if (i != 0)
                    //arcs.Add(new Arc(new Stage(this, m0: stages[i].m0, isp: 0, thrust: 0)));
                arcs.Add(new Arc(this, stages[i]));
            }

            arcs[arcs.Count-1].infinite = true;

            // allocate y0
            y0 = new double[arcIndex(arcs, arcs.Count)];

            // update initial position and guess for first arc
            double ve = g0 * stages[0].isp;
            tgo = ve * stages[0].startMass / stages[0].startThrust * ( 1 - Math.Exp(-dV/ve) );
            tgo_bar = tgo / t_scale;

            // initialize overall burn time
            y0[0] = tgo_bar;
            y0[1] = 0;

            UpdateY0(arcs);

            // seed continuity initial conditions
            yf = new double[arcs.Count*13];
            multipleIntegrate(y0, yf, arcs, initialize: true);

#if DEBUG
            Log.dbg("optimizer hT = " + hT.magnitude * r_scale * v_scale);
            Log.dbg("r_scale = {0} rTm = {1} {2}" + r_scale, rTm * r_scale,  rTm);
            Log.dbg("v_scale = {0} vTm = {1} {2}" + v_scale, vTm * v_scale,  vTm);

            for (int j = 0;j < y0.Length;j++)
                Log.dbg("bootstrap - y0[{0}] = {1}", j, y0[j]);

            Log.dbg("running optimizer");
#endif

            if ( !runOptimizer(arcs) )
            {
#if DEBUG
                for (int k = 0;k < y0.Length;k++)
                    Log.dbg("failed - y0[{0}] = {1}", k, y0[k]);
#endif

                Fatal("Target is unreachable even with infinite ISP");
                return;
            }

            Log.dbg("optimizer done");

            Solution new_sol = new Solution(t_scale, v_scale, r_scale, t0);
            multipleIntegrate(y0, new_sol, arcs, 10);

            for(int i = arcs.Count - 1; i >= 0; i--)
            {
                if ( new_sol.tgo(new_sol.t0, i) < 1 )
                {
                    /* burn is less than one second, delete it */
                    RemoveArc(arcs, i, new_sol);
                }
            }

#if DEBUG
            for (int k = 0;k < y0.Length;k++)
                Log.dbg("y0[{0}] = {1}", k, y0[k]);

            Log.dbg("arcs.Count = {0}", arcs.Count);
            Log.dbg("segments.Count = {1}", new_sol.segments.Count);
#endif

            bool insertedCoast = false;

            if (arcs.Count > 1 && !omitCoast)
            {
                InsertCoast(arcs, arcs.Count-1, new_sol);
                insertedCoast = true;
            }
            arcs[arcs.Count-1].infinite = false;

            Log.dbg("running optimizer3");

            if ( !runOptimizer(arcs) )
            {
                Fatal("Target is unreachable");
#if DEBUG
                for (int k = 0;k < y0.Length;k++)
                    Log.dbg("failed - y0[{0}] = {1}", k, y0[k]);
                Log.dbg("optimizer failed6");
#endif
                y0 = null;
                return;
            }

            Log.dbg("optimizer done");

            new_sol = new Solution(t_scale, v_scale, r_scale, t0);
            multipleIntegrate(y0, new_sol, arcs, 10);

#if DEBUG
            for (int k = 0;k < y0.Length;k++)
                Log.dbg("y0[{0}] = {1}", k, y0[k]);
#endif

            if (insertedCoast)
            {
                if ( new_sol.tgo(new_sol.t0, arcs.Count-2) < 1 )
                {
                    // coast is less than one second, delete it and reconverge
                    RemoveArc(arcs, arcs.Count-2, new_sol);

                    if ( !runOptimizer(arcs) )
                    {
                        Fatal("failed to converge after removing zero length coast");
                        return;
                    }

                    new_sol = new Solution(t_scale, v_scale, r_scale, t0);
                    multipleIntegrate(y0, new_sol, arcs, 10);
                }
            }

#if DEBUG
            for (int k = 0;k < y0.Length;k++)
                Log.dbg("new y0[{0}] = {1}", k, y0[k]);
#endif

            this.solution = new_sol;
            Log.dbg("done with bootstrap");

            yf = new double[arcs.Count*13];
            multipleIntegrate(y0, yf, arcs);

#if DEBUG
            for (int k = 0;k < yf.Length;k++)
                Log.dbg("new yf[{0}] = {1}", k, yf[k]);
#endif
        }

        public override void Optimize(double t0)
        {
            base.Optimize(t0);
            Log.dbg("r_scale = {0} rTm = {1} {2}", r_scale, rTm * r_scale, rTm);
            Log.dbg("v_scale = {0} rTm = {1} {2}", v_scale, vTm * v_scale, vTm);
        }
    }
}
