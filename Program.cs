using AGS2;

int numberOfCars = 6;
Car[] cars = new Car[numberOfCars];
for(int i = 0; i < numberOfCars - 2; i++)
{
    cars[i] = new Car(i);
}

cars[1].ReceiveInformationFrom(cars);

cars[4] = new Car(4);
cars[5] = new Car(5);
cars[1].ReceiveInformationFrom(cars);

cars[2] = new Car(2);
cars[3].SetCurrentSpeed();
cars[5].SetCurrentLocation();
cars[1].UpdateCarsInformation(cars);

cars[1].GetClosestCar();

cars[1].LogOtherCarsInfo();
cars[1].LogClosestCarInfo();
cars[1].LogCarInfo();