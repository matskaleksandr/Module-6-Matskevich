namespace Solution1
{
    internal class Task
    {
        public int id { get; set; }

        private string Name, Date;
        private int Completion;

        public string name { 
            get { return Name; }
            set { Name = value; }
        }
        public string date
        {
            get { return Date; }
            set { Date = value; }
        }
        public int completion
        {
            get { return Completion; }
            set { Completion = value; }
        }

        public Task()
        {
            // Конструктор без параметров
        }

        public Task(string Name, string Date, int Completion)
        {
            this.Name = Name;
            this.Date = Date;
            this.Completion = Completion;
        }
    }
}
