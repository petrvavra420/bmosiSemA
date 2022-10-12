﻿using MathNet.Numerics.Distributions;

class Simulace
{
    double[] poleCinnosti = new double[9];
    double cenaCesta1;
    double cenaCesta2;
    double cenaCesta3;
    double finalniCena;

    static void Main(string[] args)
    {
        string path = @"c:\temp\VystupSemA.txt";
        int[] poleVysledku = new int[100000];

        for (int i = 0; i < 100000; i++)
        {
            Simulace simulace = new Simulace();
            simulace.generujCinnosti();
            simulace.generujCenyCest();
            simulace.vyberNejlevnejsiCestu();
            simulace.zaokrouhliCestu();
            poleVysledku[i] = (int)simulace.finalniCena;
            Console.WriteLine("Cislo: " + simulace.finalniCena);
        }
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int i = 0; i < 100000; i++)
                {

                    sw.WriteLine(poleVysledku[i]);
                }
            }
        }

    }



    private void zaokrouhliCestu()
    {
        //finalniCena = (finalniCena +1) - finalniCena % 1;
        finalniCena = Math.Ceiling(finalniCena);
    }

    private void vyberNejlevnejsiCestu()
    {
        if (cenaCesta1 < cenaCesta2 && cenaCesta1 < cenaCesta3)
        {
            finalniCena = cenaCesta1;
        }
        else if (cenaCesta2 < cenaCesta1 && cenaCesta2 < cenaCesta3)
        {
            finalniCena = cenaCesta2;
        }
        else
        {
            finalniCena = cenaCesta3;
        }
    }

    private void generujCenyCest()
    {
        cenaCesta1 = poleCinnosti[0] + poleCinnosti[1] + poleCinnosti[3] + poleCinnosti[6] + poleCinnosti[8];
        cenaCesta2 = poleCinnosti[0] + poleCinnosti[2] + poleCinnosti[5] + poleCinnosti[6] + poleCinnosti[8];
        cenaCesta3 = poleCinnosti[0] + poleCinnosti[2] + poleCinnosti[4] + poleCinnosti[7] + poleCinnosti[8];
    }

    private void generujCinnosti()
    {
        poleCinnosti[0] = generujUnif(5, 10);
        poleCinnosti[1] = generujEmpiricCinnost2();
        poleCinnosti[2] = generujNorm(4, 1);
        poleCinnosti[3] = generujNorm(12, 2);
        poleCinnosti[4] = generujEmpiricCinnost5();
        poleCinnosti[5] = generujNorm(10, 3);
        poleCinnosti[6] = 3;
        poleCinnosti[7] = generujEmpiricCinnost8();
        poleCinnosti[8] = generujNorm(11, 2);

    }

    private double generujEmpiricCinnost8()
    {
        Random rand = new Random();
        double[] poleEmp = {
            1,
            1,
            1,
            rand.Next(2, 6),
            rand.Next(2, 6),
            rand.Next(2, 6),
            rand.Next(2,6),
            rand.Next(2,6),
            7,
            7
        };
        int vyber = rand.Next(10);
        return poleEmp[vyber];
    }

    private double generujEmpiricCinnost5()
    {
        Random rand = new Random();
        double[] poleEmp = {
            rand.Next(3, 6),
            rand.Next(3, 6),
            rand.Next(3, 6),
            rand.Next(3, 6),
            rand.Next(3, 6),
            rand.Next(3, 6),
            rand.Next(6, 8),
            rand.Next(8, 10),
            rand.Next(8, 10),
            rand.Next(8, 10)
        };
        int vyber = rand.Next(0,10);
        return poleEmp[vyber];
    }

    private double generujEmpiricCinnost2()
    {
        double[] poleEmp = { 5, 8, 8, 10, 10, 10, 10, 10, 10, 10 };
        Random random = new Random();
        int vyber = random.Next(0,10);
        return poleEmp[vyber];
    }

    private double generujUnif(int min, int max)
    {
        Random random = new Random();
        int num = random.Next(min, max);
        return num;
    }

    private double generujNorm(int str, int rozptyl)
    {
        MathNet.Numerics.Distributions.Normal normalDist = new Normal(str, rozptyl);
        double hodnota = normalDist.Sample();
        return hodnota;
    }
}
