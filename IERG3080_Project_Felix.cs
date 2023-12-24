// In this document, the function you will use: CheckCircumferenceTouchingBoundary (line 26), CheckWinCondition (line 44), Collision (line 52).
// In the main, I have written down some code for starting, including creating player, sun and 100 enemies. Also, there are 2 if-statement to check the orb is touching boundary or not.
// One remaining problem is I have not set the color of player and sun.


using System;
public struct Line
{
    public double startX;
    public double startY;
    public double endX;
    public double endY;
}
public class Planet
{
    public double x;
    public double y;
    public double mass;
    public double radius; // radius ^3 ~ mass
    public double v_x;
    public double v_y;
    public int color;     // 0: purple, 1: red

    public static double CalculateDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
    public static bool CheckCircumferenceTouchingBoundary(Planet planet, Line line) // return true if the orb touching the boundary
    {
        double distance = Math.Abs(
            (line.endY - line.startY) * planet.x -
            (line.endX - line.startX) * planet.y +
            line.endX * line.startY - line.endY * line.startX
        ) / CalculateDistance(line.startX, line.startY, line.endX, line.endY);

        return distance <= planet.radius;
    }

    public static bool CheckCircumferencesTouching(Planet planet1, Planet planet2)
    {
        double distance = CalculateDistance(planet1.x, planet1.y, planet2.x, planet2.y);
        double sumOfRadii = planet1.radius + planet2.radius;

        return distance <= sumOfRadii;
    }
    public static bool CheckWinCondition(Planet planet) // return true if the player wins
    {
        if (planet.mass > 512.0)
        {
            return true;
        }
   
    }
    public static int Collision(Planet planet1, Planet planet2) // return 1 if gameover, return 2 if the enemy is dead
    {
        double totalarea= Math.PI * Math.Pow(planet1.radius, 2)+Math.PI * Math.Pow(planet2.radius, 2);
        double newradius= Math.Sqrt(totalarea/Math.PI);
        double newmass= Math.Pow(newradius,3);
        double newv_x=(planet1.mass*planet1.v_x+planet2.mass*planet2.v_x)/newmass;
        double newv_y=(planet1.mass*planet1.v_y+planet2.mass*planet2.v_y)/newmass;
        if (CheckCircumferencesTouching(planet1, planet2))
        {
            if (planet1 is Sun || planet2 is Sun)
            {
                if(planet1 is Player || planet2 is Player)
                {
                    return 1;
                }
                else if(planet1 is Enemy || planet2 is Enemy)
                {
                    return 2; 
                }
            }
            else if (planet1 is Enemy && planet2 is Enemy)
            {
                if(planet1.mass>=planet2.mass)
                {
                    planet1.radius = newradius;
                    planet1.mass = newmass;
                    planet1.v_x = newv_x;
                    planet1.v_y = newv_y;
                    return 2; 
                }
                else
                {
                    planet2.radius = newradius;
                    planet2.mass = newmass;
                    planet2.v_x = newv_x;
                    planet2.v_y = newv_y;
                    return 2;
                }

                
            }
            else if (planet1 is Player && planet2 is Enemy)
            {
                if(planet1.mass>=planet2.mass)
                {
                    planet1.radius = newradius;
                    planet1.mass = newmass;
                    planet1.v_x = newv_x;
                    planet1.v_y = newv_y;
                    return 2;
                }
                else
                {
                    return 1;
                }
                
            }
            else if (planet1 is Enemy && planet2 is Player)
            {
                if(planet2.mass>=planet1.mass)
                {
                    planet2.radius = newradius;
                    planet2.mass = newmass;
                    planet2.v_x = newv_x;
                    planet2.v_y = newv_y;
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }

    }
    
}

public class Sun : Planet
{
    public Sun()
    {
        x = 270.0;
        y = 480.0;
        mass = Double.MaxValue;
        radius = 50;
        v_x = 0;
        v_y = 0;
    }
}

public class Enemy : Planet { }

public class Player : Planet 
{ 
    public Player()
    {
        x = 100;
        y = 600;
        mass = 15.625;
        radius = 2.5;
        v_x = Math.Cos(Math.Atan(y/x))*10;
        v_y = Math.Sin(Math.Atan(y/x))*10;
        
    }


}

public class Program
{
    public static double GetRandomX(Random random)
    {
        double x = random.NextDouble() * 330; // Generate a random double between 0 and 330

        if (x > 210)
        {
            x += 330; // Shift the range to 330 to 540
        }

        return x;
    }

    public static double GetRandomY(Random random)
    {
        double y = random.NextDouble() * 420; // Generate a random double between 0 and 420

        if (y > 280)
        {
            y += 260; // Shift the range to 540 to 700
        }

        return y;
    }
    
    public static void Main(string[] args)
    {
        Line line1 = new Line { startX = 0, startY = 0, endX = 540, endY = 0 };
        Line line2 = new Line { startX = 0, startY = 960, endX = 540, endY = 960 };
        Line line3 = new Line { startX = 0, startY = 0, endX = 0, endY = 960 };
        Line line4 = new Line { startX = 540, startY = 0, endX = 540, endY = 960 };
        Planet planet1 = new Sun();
        Planet planet2 = new Player();
        Random random = new Random();
        Planet[] planets = new Planet[100];

        for (int i = 0; i < 50; i++)
        {
            double radius = random.NextDouble() * 1.5 + 1; 
            double mass = Math.Pow(radius, 3); 

            double x = GetRandomX(random);
            double y = GetRandomY(random);

            double v_x = Math.Cos(Math.Atan(y/x))*10;
            double v_y = Math.Sin(Math.Atan(y/x))*10;

            planets[i] = new Planet
            {
                x = x,
                y = y,
                mass = mass,
                radius = radius,
                v_x = v_x,
                v_y = v_y,
                color = 0
            };
        }

        for (;i < planets.Length; i++)
        {
            double radius = random.NextDouble() * 2.5 + 2.5; 
            double mass = Math.Pow(radius, 3); 

            double x = GetRandomX(random);
            double y = GetRandomY(random);

            double v_x = Math.Cos(Math.Atan(y/x))*10;
            double v_y = Math.Sin(Math.Atan(y/x))*10;

            planets[i] = new Planet
            {
                x = x,
                y = y,
                mass = mass,
                radius = radius,
                v_x = v_x,
                v_y = v_y,
                color = 1
                
            };
        }
        
       
       
        
        if(Planet.CheckCircumferenceTouchingBoundary(planet2, line1) || Planet.CheckCircumferenceTouchingBoundary(planet2, line2) || Planet.CheckCircumferenceTouchingBoundary(planet2, line3) || Planet.CheckCircumferenceTouchingBoundary(planet2, line4)) // it is a if-statement for checking the player orb touching the boundary or not, return 1 if it touchs.
        {
            return 1;
        }
        else{
            return 2;
        }

         if(Planet.CheckCircumferenceTouchingBoundary(planet3, line1) || Planet.CheckCircumferenceTouchingBoundary(planet3, line2) || Planet.CheckCircumferenceTouchingBoundary(planet3, line3) || Planet.CheckCircumferenceTouchingBoundary(planet3, line4)) // it is a if-statement for checking the enemy orb touching the boundary or not, return 1 if it touchs.
        {
            return 1;
        }
        else{
            return 2;
        }

        
    }
    
}
