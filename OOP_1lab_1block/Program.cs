using System.Text;

namespace OOP_1lab_1block
{
//12. Реалізувати клас, що представляє натуральне число. Передбачити індексатор для
   // доступу до цифр цього числа, методи обернення числа, та підрахунку кількості
   // нулів.
   class NNumber
    {
        private byte[] figs { get; set; }
        public NNumber(int n)
        {
            if (n < 1) throw new ArgumentException("Число повинно бути натуральним");
            string num = n.ToString();
            figs = new byte[num.Length];
            for(int i = 0; i < num.Length; i++)
            {
                figs[i] = (byte)(num[i] - '0');
            }
        }
        public int Length { get => figs.Length; }
        public byte this[int i]
        {
            get
            {
                if(i >= 0 && i < this.Length)
                    return figs[i];
                return 0;
            }
            set
            {
                if(i >= 0 && i < this.Length)
                    figs[i] = value;
            }
        }
        public void Reverse() => Array.Reverse(figs);
        public void ReverseNormalize()// обрізає ведучі нулі.
        {
            int end = figs.Length - 1;
            while (end > 0 && figs[end] == 0)
                end--;
            byte[] newDigits = new byte[end + 1];
            for (int i = 0; i <= end; i++)
            {
                newDigits[i] = figs[end - i];
            }
            figs = newDigits;
        }
        public int CountZeros() => figs.Count(x => x == 0);
        public override string ToString() => string.Concat(figs);
    }    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            NNumber num = new NNumber(102030);

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
