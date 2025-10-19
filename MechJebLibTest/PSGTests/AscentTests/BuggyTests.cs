using System;
using MechJebLib.Functions;
using MechJebLib.Primitives;
using MechJebLib.PSG;
using MechJebLib.Utils;
using Xunit;
using Xunit.Abstractions;
using static MechJebLib.Utils.Statics;

namespace MechJebLibTest.PSGTests.AscentTests
{
    // test from buggy rockets that people have given me
    public class BuggyTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BuggyTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // Very short burn time suborbital rocket that had trouble with a high altitude convergence
        [Fact]
        public void ExtremeSuborbitalCoast()
        {
            Logger.Register(o => _testOutputHelper.WriteLine((string)o));

            var    r0    = new V3(-4821814.88567718, 2840684.21567451, 3044792.18285841);
            var    v0    = new V3(-207.145959092742, -351.61231101642, -6.44113221332937E-06);
            var    u0    = new V3(-0.756795136086477, 0.44594494863847, 0.477906107902527);
            double t0    = 222334531.385379;
            double mu    = 398600435436096;
            double rbody = 6371010;
            double attR  = rbody + 250000;
            Ascent ascent = Ascent.Builder()
                .Initial(r0, v0, u0, t0, mu, rbody)
                .SetTarget(attR, attR, attR, 0.498274048151861, 0, 0, true, false)
                .AddStageUsingFinalMass(23796.5132378765, 3243.62653856172, 241.889217870454, 127.294984318196, 4, 4)
                .AddOptimizedCoast(438.273627540912, 0, 450, 3, 3)
                .AddStageUsingFinalMass(438.273627540912, 200.596948133534, 220.000025355962, 5.82703026766676, 2, 2, true, false)
                .AddStageUsingFinalMass(124.617032384849, 59.7961198192007, 220.000054043093, 5.82703000157093, 1, 1, true, false)
                .AddStageUsingFinalMass(41.1652332654921, 19.558262410276, 220.00004748658, 5.82703000157093, 0, 0, true, false)
                .FixedBurnTime(true)
                .Build();

            ascent.Run();

            Optimizer psg = ascent.GetOptimizer() ?? throw new Exception("null optimizer");

            using Solution solution = psg.Solution ?? throw new Exception("null solution");

            psg.PrimalFeasibility.ShouldBeZero(1e-5);

            solution.Tgo(solution.T0, 0).ShouldEqual(127.29498431819599, 1e-9);
            //solution.Tgo(solution.T0, 1).ShouldEqual(235.52639456314833, 1e-2);
            solution.Tgo(solution.T0, 2).ShouldEqual(5.8270302676667614, 1e-9);
            solution.Tgo(solution.T0, 3).ShouldEqual(5.8270300015709351, 1e-9);
            solution.Tgo(solution.T0, 4).ShouldEqual(5.8270300015709351, 1e-9);
        }

        // A suborbital Thor that had convergence issues on the pad
        [Fact]
        public void SubOrbitalThor()
        {
            Logger.Register(o => _testOutputHelper.WriteLine((string)o));
            var    r0    = new V3(1470817.12150277, -5396417.72296515, 3050610.07863681);
            var    v0    = new V3(393.513790634158, 107.250777770179, 0.00161788515083675);
            var    u0    = new V3(0.23076841882078, -0.846631607518583, 0.47954249382019);
            double t0    = 666733.571923551;
            double mu    = 398600435436096;
            double rbody = 6371000;

            //double PeR = 6.371e+6 + 185e+3;
            //double ApR = 6.371e+6 + 10e+6;
            double incT = 0.499303792410538;

            Ascent ascent = Ascent.Builder()
                .Initial(r0, v0, u0, t0, mu, rbody)
                .SetTarget(6556000, 6557000, 6556000, 0.499303792410538, 0, 0, true, false)
                .AddStageUsingFinalMass(52831.1138786043, 6373.37509359599, 287.450291861922, 187.198692023839, 3, 3)
                .AddStageUsingFinalMass(2517.01227921406, 738.314292468521, 270.157637827755, 139.419410735907, 1, 1)
                .AddStageUsingFinalMass(81.5125042572618, 74.8691843417473, 250.000044703484, 0.678632409425986, 0, 0, true, false)
                .FixedBurnTime(true)
                .Build();

            ascent.Run();

            Optimizer      psg      = ascent.GetOptimizer() ?? throw new Exception("null optimizer");
            using Solution solution = psg.Solution ?? throw new Exception("null solution");

            psg.PrimalFeasibility.ShouldBeZero(1e-5);

            (V3 rf, V3 vf) = solution.TerminalStateVectors();

            (double smaf, double eccf, double incf, double lanf, double argpf, double tanof, _) =
                Astro.KeplerianFromStateVectors(mu, rf, vf);

            solution.R(t0).ShouldEqual(r0, 1e-9);
            solution.V(t0).ShouldEqual(v0, 1e-9);
            solution.M(t0).ShouldEqual(52831.1138786043, 1e-9);
            solution.Vgo(t0).ShouldEqual(9419.6763414588168, 1e-7);
            //solution.U(t0).ShouldEqual(new V3(0.75907214440924897, -0.52490464241139112, 0.38507738956853049).normalized, 1e-7);
            solution.U(t0).ShouldEqual(new V3(0.75134663024925263, -0.53345306280662119, 0.38846630612619154).normalized, 1e-3);

            smaf.ShouldEqual(8444741.8678321224, 1e-4);
            eccf.ShouldEqual(0.22365892260446163, 1e-4);
            incf.ShouldEqual(incT, 1e-4);
            lanf.ShouldEqual(3.4079089965118641, 1e-4);
            argpf.ShouldEqual(1.7701241261880956, 1e-4);
            ClampPi(tanof).ShouldBeZero(1e-7);
        }

        [Fact]
        // this is a buggy rocket, not an actual delta4 heavy, but it is being used to test that
        // some of the worst possible targets still converge and to test the fallback from failure
        // with the analytic thrust integrals to doing numerical integration.
        private void BuggyDelta4ExtremelyHeavy()
        {
            Logger.Register(o => _testOutputHelper.WriteLine((string)o));

            var    r0    = new V3(5591854.96465599, 126079.439022067, 3050616.55737457);
            var    v0    = new V3(-9.1936944030452, 407.764494724287, 0.000353003400966649);
            var    u0    = new V3(0.877712545724556, 0.0197197822130759, 0.478781640529633);
            double t0    = 81786.024077168;
            double mu    = 398600435436096;
            double rbody = 6371000;

            double decmass = 2403; // can be decreased to add more payload

            Ascent ascent = Ascent.Builder()
                .Initial(r0, v0, u0, t0, mu, rbody)
                .SetTarget(6571000, 6571000, 6571000, 0.558505360638185, 0, 0.349065850398866, true, false)
                .AddStageUsingFinalMass(731995.326793174 - decmass, 328918.196876464 - decmass, 408.091742854086, 159.080051443926, 4, 4)
                .AddStageUsingFinalMass(268744.821741949 - decmass, 168530.570801559 - decmass, 408.091702159863, 118.652885602609, 2, 2)
                .AddStageUsingFinalMass(40763.8839289468 - decmass, 13796.4420050909 - decmass, 465.500076480742, 1118.13132581707, 1, 1)
                .FixedBurnTime(false)
                .Build();

            ascent.Run();

            Optimizer psg = ascent.GetOptimizer() ?? throw new Exception("null optimizer");

            using Solution solution = psg.Solution ?? throw new Exception("null solution");

            psg.PrimalFeasibility.ShouldBeZero(1e-5);
        }

        [Fact]
        private void BiggerEarlyRocketMaybe()
        {
            Logger.Register(o => _testOutputHelper.WriteLine((string)o));
            var    r0    = new V3(-4230937.57027061, -3658393.88789034, 3050613.04457008);
            var    v0    = new V3(266.772640873606, -308.526291373473, 0.00117499917444357);
            var    u0    = new V3(-0.664193346276844, -0.574214958863673, 0.478669494390488);
            double t0    = 134391.70903729;
            double mu    = 398600435436096;
            double rbody = 6371000;

            Ascent ascent = Ascent.Builder()
                .Initial(r0, v0, u0, t0, mu, rbody)
                .SetTarget(6621000, 6623000, 6621000, 0.558505360638185, 0, 0.349065850398866, false, false)
                .AddStageUsingFinalMass(51814.6957082437, 12381.5182945184, 277.252333393375, 139.843831752395, 2, 2)
                .AddStageUsingFinalMass(9232.92402212159, 887.216491554419, 252.019434034823, 529.378964006269, 1, 1)
                .FixedBurnTime(false)
                .Build();

            ascent.Run();

            Optimizer psg = ascent.GetOptimizer() ?? throw new Exception("null optimizer");

            using Solution solution = psg.Solution ?? throw new Exception("null solution");

            psg.PrimalFeasibility.ShouldBeZero(1e-5);
        }

        [Fact]
        private void FourStagesNeedsTwo()
        {
            Logger.Register(o => _testOutputHelper.WriteLine((string)o));

            Ascent ascent = Ascent.Builder()
                .Initial(new V3(-2750001.81262551, -4870536.29021665, 3050610.57762817), new V3(355.169076284135, -200.527924207868, -0.00280072807318912), new V3(-0.431553114381181, -0.764704888551875, 0.478527367115021), 139051.267268641, 398600435436096, 6371000)
                .SetTarget(6551000, 6551000, 6551000, 0.499303792410538, 0, 0, false, false)
                //.SetTarget(7751000, 7751000, 7051000, 0.499303792410538, 0, 0, false, false)
                .AddStageUsingFinalMass(176568.069166482, 33739.9978121645, 294.100105371403, 150.314019929573, 7, 7)
                .AddStageUsingFinalMass(22640.1444759873, 7657.9646078915, 427.000116464637, 470.292183477623, 4, 4)
                .AddStageUsingFinalMass(5155.6086446711, 1421.59054825615, 427.000111546093, 234.42243240551, 2, 2)
                .AddStageUsingFinalMass(529.255624515231, 306.288652498034, 279.000062751027, 17.4300000004006, 0, 0)
                .FixedBurnTime(false)
                .Build();

            ascent.Run();

            Optimizer psg = ascent.GetOptimizer() ?? throw new Exception("null optimizer");

            using Solution solution = psg.Solution ?? throw new Exception("null solution");

            solution.Bt(0, 0).ShouldEqual(150.314019929573, 1e-6);
            solution.Bt(1, 0).ShouldEqual(470.292183477623, 1e-4);
            solution.Bt(2, 0).ShouldNotBeZero();
            solution.Bt(3, 0).ShouldBeZero(1e-4);

            psg.PrimalFeasibility.ShouldBeZero(1e-5);
        }
    }
}
