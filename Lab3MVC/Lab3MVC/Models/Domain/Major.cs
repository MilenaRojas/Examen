namespace Lab3MVC.Models.Domain
{
    public class Major
    {
        private int id;
        private string code;
        private string name;

        public Major()
        {
        }

        public Major(int id, string code, string name)
        {
            this.id = id;
            this.code = code;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
    }
}
