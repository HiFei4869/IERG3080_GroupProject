using System;

/// <summary>
/// Ejection code
/// </summary>
namespace PhysicEngine.Movement
{
	public class Movement
	{
        public static double EjectDirection(Planet planet, double x_c, double y_c)
        {
            double deltaX = x_c - planet.x;
            double deltaY = y_c - planet.y;
            double direction = (double)Math.Atan2(deltaY, deltaX);
            return direction;
        }
        static double NthRoot(double A, int N)
        {
            return Math.Pow(A, 1.0 / N);
        }
        public static double Mass_To_Radius(double mass)
        {
            double Radius = 0.0;
            Radius = NthRoot(3 * mass / (4 * Math.PI), 3); // assume rho = 1
            return Radius;
        }
		public static void SplitEject(ref Planet planet, double x_c, double y_c, double clickTime)
        {
            double direction = EjectDirection(planet, x_c, y_c);
            double v_e_x = v_e * Math.Cos(direction);  // x_axis velocity of the ejected mass
            double v_e_y = v_e * Math.Sin(direction);  // y_axis velocity of the ejected mass
            double v_x_1 = (planet.mass * planet.v_x - m_e * clickTime * v_e_x) / (planet.mass - m_e * clickTime);
            double v_y_1 = (planet.mass * planet.v_y - m_e * clickTime * v_e_y) / (planet.mass - m_e * clickTime);
            planet.mass = planet.mass - m_e * clickTime;
            planet.radius = Mass_To_Radius(planet.mass);
            planet.v_x = v_x_1;
            planet.v_y = v_y_1;
        }
        // For reference only.
        public struct Planet
        {
            public double x;
            public double y;
            public double mass;
            public double radius; // radius ^3 ~ mass
            public double v_x;
            public double v_y;
            public int color;     // 0: purple, 1: red
        }
        public static double v_e = 4; // velocity of ejected mass
        public static double m_e = 1; // mass ejected in unit time
    }
}

