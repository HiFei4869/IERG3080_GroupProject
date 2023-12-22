# IERG3080_GroupProject



**For reference:**


    public struct Planet
	{
	    public double x;      // position x
	    public double y;      // position y
	    public double mass;   // the mass
	    public double radius; // the radius (radius ^3 ~ mass)
	    public double v_x;    // the velocity along x_axis
	    public double v_y;    // the velocity along y_axis
	    public int color;     // 0: purple, 1: red
	}


**Movement.cs (SYF)**


**Assumption in Movement.cs:**


The sun is at (0.0, 0.0).


mass_sun >> mass_planet


**Functions of Movement.cs:**


SplitEject:


    input: planet (struct), position of mouse click (double), time of clicking (double)
    output: void (update the two velocity, planet radius and mass in the Planet struct)



FindEllipticOrbit (oval orbit):


    input: planet (struct)
    output: the new distance from the sun (stable orbit)


FindCircleOrbit (oval orbit):


    input: planet (struct)
    output: the new radius of the orbit (stable orbit)



**最终ddl 12.29**
