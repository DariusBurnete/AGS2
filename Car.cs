using System.Globalization;

namespace AGS2
{
    public struct CarInfo
    {
        public string manufacturer;
        public string model;
        public int currentSpeed;
        public (int X, int Y) currentLocation;
        public string[] eventsIn100Km;
    }

    public class Car
    {
        private CarInfo info;
        private int uniqueID;

        private int manufacturerRandom;
        private int modelRandom;
        private int eventsRandom;

        private Dictionary<int, CarInfo>? otherCarsInfo;
        private int closestCar;

        public Car(int id)
        {
            SetCarInfo(id);
            SetCurrentSpeed();
            SetCurrentLocation();
            SetEvents();
        }

        // Setters

        private void SetManufacturer()
        {
            manufacturerRandom = new Random().Next(Libs.manufacturers.Length);
            info.manufacturer = Libs.manufacturers[manufacturerRandom];
        }

        private void SetModel()
        {
            modelRandom = new Random().Next(Libs.models.Length);
            info.model = Libs.models[modelRandom];
        }

        private void SetUniqueID(int id)
        {
            uniqueID = id;
        }

        private void SetCarInfo(int id)
        {
            SetManufacturer();
            SetModel();
            SetUniqueID(id);
        }

        public void SetCurrentSpeed()
        {
            info.currentSpeed = new Random().Next(0, Libs.maxSpeed);
        } 

        public void SetCurrentLocation()
        {
            info.currentLocation = (
                new Random().Next(0, Libs.maxCoordinates), 
                new Random().Next(0, Libs.maxCoordinates)
            );
        }

        public void SetEvents()
        {
            eventsRandom = new Random().Next(5, Libs.events.Length / 4);
            info.eventsIn100Km = new string[eventsRandom];
            for (int i = 0; i < eventsRandom; i++)
            {
                info.eventsIn100Km[i] = Libs.events[new Random().Next(0, Libs.events.Length)];
            }
        }

        // Systems

        public void ReceiveInformationFrom(Car[] otherCars)
        {
            foreach (Car otherCar in otherCars)
            {
                if (otherCar == null)
                    continue;
                if (otherCarsInfo != null && otherCarsInfo.ContainsKey(otherCar.uniqueID))
                    continue;
                if (otherCar.uniqueID == this.uniqueID)
                    continue;
                this.ReceiveInformationFrom(otherCar);
            }
        }

        public void UpdateCarsInformation(Car[] otherCars)
        {
            foreach (Car otherCar in otherCars)
            {
                if (otherCar == null)
                    continue;
                if (otherCar.uniqueID == this.uniqueID)
                    continue;
                if (!otherCarsInfo.ContainsKey(otherCar.uniqueID))
                    continue;
                CarInfo tempInfo = otherCarsInfo[otherCar.uniqueID];
                ModifyOnCondition(otherCar.info, ref tempInfo);
                otherCarsInfo[otherCar.uniqueID] = tempInfo; 
            }
        }

        private static void ModifyOnCondition(CarInfo otherCarInfo, ref CarInfo tempInfo)
        {
            if (otherCarInfo.manufacturer != tempInfo.manufacturer)
            {
                tempInfo.manufacturer = otherCarInfo.manufacturer;
            }
            if (otherCarInfo.model != tempInfo.model)
            {
                tempInfo.model = otherCarInfo.model;
            }
            if (otherCarInfo.currentSpeed != tempInfo.currentSpeed)
            {
                tempInfo.currentSpeed = otherCarInfo.currentSpeed;
            }
            if (otherCarInfo.currentLocation != tempInfo.currentLocation)
            {
                tempInfo.currentLocation = otherCarInfo.currentLocation;
            }
            if (otherCarInfo.eventsIn100Km != tempInfo.eventsIn100Km)
            {
                tempInfo.eventsIn100Km = otherCarInfo.eventsIn100Km;
            }
        }

        private void ReceiveInformationFrom(Car otherCar)
        {
            otherCarsInfo ??= new Dictionary<int, CarInfo>();
            otherCarsInfo[otherCar.uniqueID] = otherCar.info;
        }

        public void GetClosestCar()
        {
            double closestDistance = double.MaxValue;
            foreach (var otherCarInfo in otherCarsInfo)
            {
                double distance = CalculateDistanceTo(otherCarInfo.Value);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCar = otherCarInfo.Key;
                }
            }
        }

        private double CalculateDistanceTo(CarInfo otherCarInfo)
        {
            double xDiff = otherCarInfo.currentLocation.X - this.info.currentLocation.X;
            double yDiff = otherCarInfo.currentLocation.Y - this.info.currentLocation.Y;
            double distance  = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return distance;
        }

        // Logs

        public void LogOtherCarsInfo()
        {
            if (otherCarsInfo == null)
                return;

            foreach (var otherCarInfo in otherCarsInfo)
            {
                Console.WriteLine($"Car {otherCarInfo.Key} ");
                Console.WriteLine($"    Manufacturer: {otherCarInfo.Value.manufacturer}");
                Console.WriteLine($"    Model: {otherCarInfo.Value.model}");
                Console.WriteLine($"    Current Speed: {otherCarInfo.Value.currentSpeed}");
                Console.WriteLine($"    Current Location: {otherCarInfo.Value.currentLocation}");
                Console.WriteLine($"    Events in 100Km: {string.Join(", ", otherCarInfo.Value.eventsIn100Km)}");
                Console.WriteLine();
            }
        }

        public void LogClosestCarInfo()
        {
            if (otherCarsInfo == null)
                return;

            Console.WriteLine($"Closest Car {closestCar}");
            Console.WriteLine($"    Manufacturer: {otherCarsInfo[closestCar].manufacturer}");
            Console.WriteLine($"    Model: {otherCarsInfo[closestCar].model}");
            Console.WriteLine($"    Current Speed: {otherCarsInfo[closestCar].currentSpeed}");
            Console.WriteLine($"    Current Location: {otherCarsInfo[closestCar].currentLocation}");
            Console.WriteLine($"    Events in 100Km: {string.Join(", ", otherCarsInfo[closestCar].eventsIn100Km)}");
            Console.WriteLine();
        }

        public void LogCarInfo()
        {
            Console.WriteLine($"Car {uniqueID} ");
            Console.WriteLine($"    Manufacturer: {info.manufacturer}");
            Console.WriteLine($"    Model: {info.model}");
            Console.WriteLine($"    Current Speed: {info.currentSpeed}");
            Console.WriteLine($"    Current Location: {info.currentLocation}");
            Console.WriteLine($"    Events in 100Km: {string.Join(", ", info.eventsIn100Km)}");
            Console.WriteLine();
        }
    }
}