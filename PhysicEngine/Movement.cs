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

        public static double v_e = 4; // velocity of ejected mass
        public static double m_e = 1; // mass ejected in unit time
    }
    public class Orbit
    {
        public static double G = 10;         // gravitational constant
        public static double mass_sun = 100; // mass of sun; can be modified
        // Assume the stable orbit is a circle.
        public static double FindCircleOrbit(Planet planet)
        {
            double radius_orbit_new = 0.0;
            double velocitySquare = Math.Pow(planet.v_x, 2) + Math.Pow(planet.v_y, 2);
            radius_orbit_new = G * mass_sun / velocitySquare;
            return radius_orbit_new;
        }
        // Assume the stable orbit is an oval.
        // The orbit: v^2 = G*M*(2/d-1/a); assume mass_sun >> mass_planet.
        // Assume the sun is at the origin.
        public static double FindEllipticOrbit(Planet planet)
        {
            double distance_from_sun_new = 0.0;
            double velocitySquare = Math.Pow(planet.v_x,2)+Math.Pow(planet.v_y,2);
            double distance_from_sun_old = Math.Sqrt(planet.x * planet.x + planet.y * planet.y);
            double semi_major_axis = G * mass_sun * distance_from_sun_old / (2 * G * mass_sun - distance_from_sun_old * velocitySquare);
            distance_from_sun_new = 2/(velocitySquare/(G*mass_sun)+1/ semi_major_axis);
            return distance_from_sun_new;
        }
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
}

