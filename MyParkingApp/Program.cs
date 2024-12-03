using System;
using System.Collections.Generic;

class Program
{
    static List<Vehicle> lotMobil = new List<Vehicle>();
    static List<Vehicle> lotMotor = new List<Vehicle>();
    const int hargaMobil = 5000;
    const int hargaMotor = 2000;

    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        Console.WriteLine("\nMasukan pilihan :");
        Console.WriteLine("1. Kendaraan masuk");
        Console.WriteLine("2. Kendaraan keluar");
        Console.WriteLine("3. Cek lot parkir kendaraan");
        Console.Write("Pilih: ");
        int pilihan;
        if (int.TryParse(Console.ReadLine(), out pilihan))
        {
            switch (pilihan)
            {
                case 1:
                    CheckIn();
                    break;
                case 2:
                    CheckOut();
                    break;
                case 3:
                    LaporanKendaraan();
                    break;
                case 4:
                    Console.WriteLine("Terima kasih!");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    Menu();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Input tidak valid.");
            Menu();
        }
    }

    static void CheckIn()
    {
        Console.Write("Tipe Kendaraan (Mobil/Motor): ");
        string jenis = Console.ReadLine();
        Console.Write("Plat nomor kendaraan: ");
        string plat = Console.ReadLine().ToUpper();
        Console.Write("Total Jam Parkir: ");
        int jam;
        if (int.TryParse(Console.ReadLine(), out jam))
        {
            int harga = jenis.ToLower() == "motor" ? hargaMotor * jam : hargaMobil * jam;
            bool status = false; // Kendaraan baru check-in
            var kendaraan = new Vehicle(plat, jenis);
            kendaraan.CheckIn(plat, jenis, harga, status);
            Console.WriteLine("kendaraan berhasil masuk!");
        }
        else
        {
            Console.WriteLine("Input jam parkir tidak valid.");
        }
        Menu();
    }

    static void CheckOut()
    {
        Console.Write("Plat Nomor Kendaraan: ");
        string plat = Console.ReadLine().ToUpper();
        Console.Write("Tipe Kendaraan (Mobil/Motor): ");
        string jenis = Console.ReadLine();
        var kendaraan = new Vehicle(plat, jenis);
        kendaraan.CheckOut(plat, jenis);
        Console.WriteLine("Kendaraan berhasil keluar!");
        Menu();
    }

    static void LaporanKendaraan()
    {
        Console.WriteLine("Cek lot kendaraan:");
        Console.WriteLine("Lot Mobil:");
        foreach (var mobil in lotMobil)
        {
            Console.WriteLine($"{mobil.Plat} ({mobil.Jenis}) - Harga : {mobil.Harga}");
        }

        Console.WriteLine("Lot Motor:");
        foreach (var motor in lotMotor)
        {
            Console.WriteLine($"{motor.Plat} ({motor.Jenis}) - Harga: {motor.Harga}");
        }
        Menu();
    }

    class Vehicle
    {
        public string Plat { get; set; }
        public string Jenis { get; set; }
        public int Harga { get; set; }
        public bool Status { get; set; }

        public Vehicle(string plat, string jenis)
        {
            Plat = plat;
            Jenis = jenis;
        }

        public void CheckIn(string plat, string jenis, int harga, bool status)
        {
            if (jenis.ToLower() == "motor")
            {
                lotMotor.Add(new Vehicle(plat, jenis) { Harga = harga, Status = status });
            }
            else if (jenis.ToLower() == "mobil")
            {
                lotMobil.Add(new Vehicle(plat, jenis) { Harga = harga, Status = status });
            }
            else
            {
                Console.WriteLine("Jenis kendaraan tidak dikenali.");
            }
        }

        public void CheckOut(string plat, string jenis)
        {
            if (jenis.ToLower() == "motor")
            {
                lotMotor.RemoveAll(item => item.Plat == plat);
            }
            else if (jenis.ToLower() == "mobil")
            {
                lotMobil.RemoveAll(item => item.Plat == plat);
            }
            else
            {
                Console.WriteLine("Jenis kendaraan tidak dikenali.");
            }
        }

        public void CheckFull()
        {
            if (lotMobil.Count >= 20 && lotMotor.Count >= 20)
            {
                Console.WriteLine("Lot parkir sudah penuh!");
            }
            else
            {
                Console.WriteLine("Masih ada slot parkir tersedia.");
            }
        }
    }
}
