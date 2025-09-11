namespace OOP_1lab_2block
{
    class TCircle
    {
        private double Radius { get; set; } = 1;
        public TCircle()
        {
            Radius = 1;
        }
        public TCircle(double radius)
        {
            this.Radius = radius;
        }
        public double FindArea() => Radius * Radius * Math.PI;
        public double FindLength() => 2 * Radius * Math.PI;
    }
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
