namespace OOP_1lab_2block
{
    public class TCircle : IComparable<TCircle>
    {
        protected double Radius { get; }
        public TCircle()
        {
            Radius = 1;
        }
        public TCircle(double radius)
        {
            if (radius < 0) throw new ArgumentException("Радіус не може бути від'ємним");
            this.Radius = radius;
        }
        public TCircle(TCircle circle)
        {
            this.Radius = circle.Radius;
        }
        public double AreaOfCircle() => Radius * Radius * Math.PI;
        public double LengthOfCircle() => 2 * Radius * Math.PI;
        public int CompareTo(TCircle otherCircle) => this.Radius.CompareTo(otherCircle.Radius);
        public override string ToString()
        {
            return $"Радіус: {Radius}";
        }
        public double GetRadius() => Radius;
        public static TCircle operator +(TCircle c1, TCircle c2) => new TCircle(c1.Radius + c2.Radius);
        public static TCircle operator -(TCircle c1, TCircle c2)
        {
            double newRadius = c1.Radius - c2.Radius;
            if (newRadius <= 0) throw new ArgumentException("Радіус не може бути від'ємним");
            return new TCircle(newRadius);
        }
        public static TCircle operator *(TCircle c, double k)
        {
            return new TCircle(c.Radius * k);
        }
        public static TCircle operator *(double k, TCircle c)
        {
            return new TCircle(c.Radius * k);
        }
        //замість перевизначення операторів можна:
        //public static implicit operator double(TCircle c) => c.Radius;
        //public static implicit operator TCircle(double r) => new TCircle(r);
    }
    public class TCylinder : TCircle
    {
        public TCylinder() : base()
        {
            Height = 1;
        }
        public TCylinder(double radius, double height) : base(radius)
        {
            if (radius < 0) throw new ArgumentException("Висота не може бути від'ємнa");
            this.Height = height;
        }
        public TCylinder(TCylinder cylinder) : base(cylinder.Radius)
        {
            this.Height = cylinder.Height;
        }
        private double Height { get; }
        public double Volume() => AreaOfCircle() * Height;
        public double SurfaceArea() => 2 * AreaOfCircle() + LengthOfCircle() * Height;
        public override string ToString()
        {
            return $"Циліндр - Радіус: {Radius:F2}, Висота: {Height:F2}";
        }
        public double GetHeight() => Height;
    }
    internal class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                ShowMainMenu();

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        WorkWithCircles();
                        break;
                    case "2":
                        WorkWithCylinders();
                        break;
                    case "3":
                        Console.WriteLine("Програма завершена. До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір! Натисніть будь-яку клавішу для продовження...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           ГОЛОВНЕ МЕНЮ                   ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║  1. Робота з колами (TCircle)            ║");
            Console.WriteLine("║  2. Робота з циліндрами (TCylinder)      ║");
            Console.WriteLine("║  3. Вихід                                ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("\nВаш вибір (1-3): ");
        }

        static void WorkWithCircles()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            РОБОТА З КОЛАМИ               ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            TCircle circle1, circle2;

            if (ChooseInputMethod())
            {
                circle1 = new TCircle(random.Next(0, 100));
                circle2 = new TCircle(random.Next(0, 100));

                Console.WriteLine($"\n Згенеровані випадкові кола:");
                Console.WriteLine($"Коло 1 - радіус: {circle1.GetRadius()}");
                Console.WriteLine($"Коло 2 - радіус: {circle2.GetRadius()}");
            }
            else
            {
                circle1 = CreateCircleManually("першого");
                circle2 = CreateCircleManually("другого");
            }

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("РЕЗУЛЬТАТИ ОБЧИСЛЕНЬ ДЛЯ КІЛ:");
            Console.WriteLine(new string('=', 50));

            DisplayCircleInfo(circle1, "Коло 1");
            Console.WriteLine();
            DisplayCircleInfo(circle2, "Коло 2");

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("ОПЕРАЦІЇ З КОЛАМИ:");
            Console.WriteLine(new string('-', 50));

            try
            {
                TCircle sumCircle = circle1 + circle2;
                Console.WriteLine($" Сума радіусів: {sumCircle}");
                Console.WriteLine($" Площа трикутника з новим радіусом: {sumCircle.AreaOfCircle():F2}");

                if (circle1.CompareTo(circle2) > 0)
                {
                    TCircle diffCircle = circle1 - circle2;
                    Console.WriteLine($" Різниця радіусів (Коло1 - Коло2): {diffCircle}");
                }
                else if (circle2.CompareTo(circle1) > 0)
                {
                    TCircle diffCircle = circle2 - circle1;
                    Console.WriteLine($" Різниця радіусів (Коло2 - Коло1): {diffCircle}");
                }
                else
                {
                    Console.WriteLine(" Радіуси однакові, різниця дорівнює 0");
                }

                TCircle multCircle = circle1 * 2.5;
                Console.WriteLine($" Коло1 × 2.5: {multCircle}");

                int comparison = circle1.CompareTo(circle2);
                Console.WriteLine($" Порівняння кіл: {GetComparisonResult(comparison)}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($" Помилка при операції: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter для повернення до меню...");
            Console.ReadKey();
        }

        static void WorkWithCylinders()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║          РОБОТА З ЦИЛІНДРАМИ             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            TCylinder cylinder1, cylinder2;

            if (ChooseInputMethod())
            {
                cylinder1 = new TCylinder(random.Next(0, 100), random.Next(0, 100));
                cylinder2 = new TCylinder(random.Next(0, 100), random.Next(0, 100));

                Console.WriteLine($"\n Згенеровані випадкові циліндри:");
                Console.WriteLine($"Циліндр 1 - радіус: {cylinder1.GetRadius()}, висота: {cylinder1.GetHeight()}");
                Console.WriteLine($"Циліндр 2 - радіус: {cylinder2.GetRadius()}, висота: {cylinder2.GetHeight()}");
            }
            else
            {
                cylinder1 = CreateCylinderManually("першого");
                cylinder2 = CreateCylinderManually("другого");
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("РЕЗУЛЬТАТИ ОБЧИСЛЕНЬ ДЛЯ ЦИЛІНДРІВ:");
            Console.WriteLine(new string('=', 60));

            DisplayCylinderInfo(cylinder1, "Циліндр 1");
            Console.WriteLine();
            DisplayCylinderInfo(cylinder2, "Циліндр 2");

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("ПОРІВНЯННЯ ЦИЛІНДРІВ:");
            Console.WriteLine(new string('-', 60));

            int comparison = cylinder1.CompareTo(cylinder2);
            Console.WriteLine($"Порівняння за радіусом: {GetComparisonResult(comparison)}");

            if (cylinder1.Volume() > cylinder2.Volume())
                Console.WriteLine($"За об'ємом: Циліндр 1 більший за Циліндр 2");
            else if (cylinder1.Volume() < cylinder2.Volume())
                Console.WriteLine($"За об'ємом: Циліндр 2 більший за Циліндр 1");
            else
                Console.WriteLine($"За об'ємом: Циліндри однакові");

            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("ДЕМОНСТРАЦІЯ КОНСТРУКТОРА КОПІЮВАННЯ:");
            Console.WriteLine(new string('-', 60));

            TCylinder cylinderCopy = new TCylinder(cylinder1);
            Console.WriteLine($"Копія Циліндра 1: {cylinderCopy}");
            Console.WriteLine($"Об'єм копії: {cylinderCopy.Volume():F2}");

            Console.WriteLine("\nНатисніть Enter для повернення до меню...");
            Console.ReadKey();
        }

        static bool ChooseInputMethod()
        {
            while (true)
            {
                Console.WriteLine("\nОберіть спосіб введення параметрів:");
                Console.WriteLine("1. Випадкові значення");
                Console.WriteLine("2. Ввести вручну");
                Console.Write("\nВаш вибір (1-2): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return true;
                    case "2":
                        return false;
                    default:
                        Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                        break;
                }
            }
        }

        static TCircle CreateCircleManually(string description)
        {
            while (true)
            {
                try
                {
                    Console.Write($"\n Введіть радіус {description} кола: ");
                    double radius = Convert.ToDouble(Console.ReadLine());
                    return new TCircle(radius);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка! Введіть числове значення.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        static TCylinder CreateCylinderManually(string description)
        {
            while (true)
            {
                try
                {
                    Console.Write($"\nВведіть радіус {description} циліндра: ");
                    double radius = Convert.ToDouble(Console.ReadLine());

                    Console.Write($"Введіть висоту {description} циліндра: ");
                    double height = Convert.ToDouble(Console.ReadLine());

                    return new TCylinder(radius, height);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка! Введіть числові значення.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        static void DisplayCircleInfo(TCircle circle, string name)
        {
            Console.WriteLine($"{name}: {circle}");
            Console.WriteLine($"  Площа: {circle.AreaOfCircle():F2}");
            Console.WriteLine($"  Довжина окружності: {circle.LengthOfCircle():F2}");
        }

        static void DisplayCylinderInfo(TCylinder cylinder, string name)
        {
            Console.WriteLine($"{name}: {cylinder}");
            Console.WriteLine($"  Площа основи: {cylinder.AreaOfCircle():F2}");
            Console.WriteLine($"  Довжина окружності: {cylinder.LengthOfCircle():F2}");
            Console.WriteLine($"  Об'єм: {cylinder.Volume():F2}");
            Console.WriteLine($"  Площа поверхні: {cylinder.SurfaceArea():F2}");
        }

        static string GetComparisonResult(int comparison)
        {
            return comparison switch
            {
                > 0 => "Перший більший за другий",
                < 0 => "Другий більший за перший",
                _ => "Однакові"
            };
        }
    }
}
