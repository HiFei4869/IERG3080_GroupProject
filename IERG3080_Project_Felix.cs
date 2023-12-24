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
    public static bool CheckCircumferenceTouchingBoundary(Planet planet, Line line)
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
    public static void CheckWinCondition(Planet planet)
    {
        if (planet.mass > 100.0)
        {
            Console.WriteLine("You win!");
            Environment.Exit(0);
        }
   
    }
    public static void Collision(Planet planet1, Planet planet2)
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
                    Console.WriteLine("Gameover!");
                    Environment.Exit(0);
                }
                else if(planet1 is Enemy || planet2 is Enemy)
                {
                    Console.WriteLine("Enemy dead!"); //This should be changed to discard the enemy planet in linked list
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
                    Console.WriteLine("Enemy dead!"); //This should be changed to discard the enemy planet2 in linked list
                }
                else
                {
                    planet2.radius = newradius;
                    planet2.mass = newmass;
                    planet2.v_x = newv_x;
                    planet2.v_y = newv_y;
                    Console.WriteLine("Enemy dead!"); //This should be changed to discard the enemy planet1 in linked list
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
                    Console.WriteLine("Enemy dead!"); //This should be changed to discard the enemy planet in linked list
                }
                else
                {
                    Console.WriteLine("Gameover!");
                    Environment.Exit(0);
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
                    Console.WriteLine("Enemy dead!"); //This should be changed to discard the enemy planet in linked list
                }
                else
                {
                    Console.WriteLine("Gameover!");
                    Environment.Exit(0);
                }
            }
        }

    }
    
}

public class Sun : Planet
{
    public Sun()
    {
        x = 0;
        y = 0;
    }
}

public class Enemy : Planet { }

public class Player : Planet { }

public class Program
{
    
    public static void Main(string[] args)
    {
        Line line1 = new Line { startX = 0, startY = 0, endX = 540, endY = 0 };
        Line line2 = new Line { startX = 0, startY = 960, endX = 540, endY = 960 };
        Line line3 = new Line { startX = 0, startY = 0, endX = 0, endY = 960 };
        Line line4 = new Line { startX = 540, startY = 0, endX = 540, endY = 960 };
        Planet planet1 = new Sun { radius = 5};
        Planet planet2 = new Player { x = 6.0, y = 6.0, mass = 1728.0, radius = 12, v_x = -2.0, v_y = 3.0, color = 1 };
        Planet planet3 = new Enemy { x = 10.0, y = 10.0, mass = 125.0, radius = 5, v_x = 2.0, v_y = -3.0, color = 1 };
        
       
        Planet.Collision(planet1,planet2);
        Planet.Collision(planet1,planet3);
        PrintPlanet(planet2);
        
        if(Planet.CheckCircumferenceTouchingBoundary(planet1, line1) || Planet.CheckCircumferenceTouchingBoundary(planet1, line2) || Planet.CheckCircumferenceTouchingBoundary(planet1, line3) || Planet.CheckCircumferenceTouchingBoundary(planet1, line4))
        {
            Console.WriteLine("Gameover!");
            Environment.Exit(0);
        }

        
    }
    public static void PrintPlanet(Planet planet)
    {
        Console.WriteLine("Planet Properties:");
        Console.WriteLine("x: " + planet.x);
        Console.WriteLine("y: " + planet.y);
        Console.WriteLine("mass: " + planet.mass);
        Console.WriteLine("radius: " + planet.radius);
        Console.WriteLine("v_x: " + planet.v_x);
        Console.WriteLine("v_y: " + planet.v_y);
        Console.WriteLine("color: " + planet.color);
        Console.WriteLine();
    }
}