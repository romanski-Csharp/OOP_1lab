using System.Text;

namespace OOP_1lab_1block
{
//12. Реалізувати клас, що представляє натуральне число. Передбачити індексатор для
   // доступу до цифр цього числа, методи обернення числа, та підрахунку кількості
   // нулів.
   class NNumber
    {
        private byte[] Figs { get; set; }
        public NNumber(int n)
        {
            if (n < 1) throw new ArgumentException("Число повинно бути натуральним");
            string num = n.ToString();
            Figs = new byte[num.Length];
            for(int i = 0; i < num.Length; i++)
            {
                Figs[i] = (byte)(num[i] - '0');
            }
        }
        public int Length { get => Figs.Length; }
        public byte this[int i]
        {
            get
            {
                if(i >= 0 && i < this.Length)
                    return Figs[i];
                return 0;
            }
            set
            {
                if(i >= 0 && i < this.Length)
                    Figs[i] = value;
            }
        }
        public void Reverse() => Array.Reverse(Figs);
        public void ReverseNormalize()// обрізає ведучі нулі.
        {
            int end = Figs.Length - 1;
            while (end > 0 && Figs[end] == 0)
                end--;
            byte[] newDigits = new byte[end + 1];
            for (int i = 0; i <= end; i++)
            {
                newDigits[i] = Figs[end - i];
            }
            Figs = newDigits;
        }
        public int CountZeros() => Figs.Count(x => x == 0);
        public override string ToString() => string.Concat(Figs);
    }    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Виберіть режим введенння: \n1 - Вручну \n2 - Random ");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
                Console.Write("Введіть число: ");

            NNumber num = new NNumber(choice == 1 ? int.Parse(Console.ReadLine()) : new Random().Next(1, 100000000));

            Console.WriteLine($"Число: {num}");         // 102030

            Console.WriteLine($"Друга цифра: {num[1]}"); // 0

            num[1] = 9;// замінимо другу цифру
            Console.WriteLine($"Після зміни: {num}");   // 192030

            num.Reverse();
            Console.WriteLine($"Обернене: {num}");      // 030291

            Console.WriteLine($"Кількість нулів: {num.CountZeros()}"); // 2
        }
    }
}
