namespace AGS2
{
    public static class Libs
    {
        public static readonly string[] manufacturers = ["Toyota", "Honda", "Ford", "Chevrolet", "Nissan", "Suzuki"];
        public static readonly string[] models = ["Corolla", "RAV4", "Yaris", "Civic", "Mustang", "Camaro", "Altima", "Swift"];
        public static readonly string[] events = [
            "Bumper", "Hole", "Brake check", "Running animal", "Running person", 
            "Pedestrian", "Accident", "Collision", "Crash", "Traffic jam", 
            "Road work", "Weather hazard", "Mechanical failure", 
            "Emergency vehicle", "Police chase", "Road rage", "Speeding",
            "Tailgating", "Illegal turn", "Wrong way", "Drunk driver",
            "Road closure", "Detour", "Traffic light malfunction", "Railroad crossing", 
            "Fallen tree", "Flooding", "Landslide", "Avalanche", "Tornado",
            "Hailstorm", "Snowstorm", "Ice on road", "Foggy conditions",
            "High winds", "Dust storm", "Volcanic eruption", "Earthquake",
            "Tsunami", "Hurricane", "Tornado warning", "Amber alert"
        ];
        public static readonly int maxSpeed = 201;
        public static readonly int maxCoordinates = 1001;
        
    }
}