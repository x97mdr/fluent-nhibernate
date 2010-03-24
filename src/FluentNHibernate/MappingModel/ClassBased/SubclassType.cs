namespace FluentNHibernate.MappingModel.ClassBased
{
    public class SubclassType
    {
        public static readonly SubclassType Subclass = new SubclassType("subclass");
        public static readonly SubclassType JoinedSubclass = new SubclassType("joined-subclass");
        public static readonly SubclassType Unknown = new SubclassType("unknown");

        readonly string elementName;

        private SubclassType(string elementName)
        {
            this.elementName = elementName;
        }

        public string GetElementName()
        {
            return elementName;
        }

        public override bool Equals(object obj)
        {
            if (obj is SubclassType)
                return ((SubclassType)obj).elementName.Equals(elementName);

            return false;
        }

        public override string ToString()
        {
            return "Subclass(" + elementName + ")";
        }

        public bool Equals(SubclassType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.elementName, elementName);
        }

        public override int GetHashCode()
        {
            return (elementName != null ? elementName.GetHashCode() : 0);
        }
    }
}