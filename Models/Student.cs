namespace Models
{
    public class Student: ICloneable
    {
        public string StudentCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int BirthYear { get; set; }

        public string Major { get; set; } = string.Empty;


        public override string ToString()
        {
            return $"[{StudentCode}] {FullName} - Age: {BirthYear} - Major: {Major}";
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
