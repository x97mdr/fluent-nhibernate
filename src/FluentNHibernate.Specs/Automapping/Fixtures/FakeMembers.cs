using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace FluentNHibernate.Specs.Automapping.Fixtures
{
    public static class FakeMembers
    {
        public static readonly Type Type = typeof(Target);
        public static readonly Member IListOfStrings;
        public static readonly Member IListOfInts;
        public static readonly Member IListOfDoubles;
        public static readonly Member IListOfShorts;
        public static readonly Member IListOfLongs;
        public static readonly Member IListOfFloats;
        public static readonly Member IListOfBools;
        public static readonly Member IListOfDateTimes;
        public static readonly Member IListOfTargets;
        public static readonly Member ISetOfTargets;
        public static readonly Member String;
        public static readonly Member Entity;

        static FakeMembers()
        {
            IListOfStrings = typeof(Target).GetProperty("IListOfStrings").ToMember();
            IListOfInts = typeof(Target).GetProperty("IListOfInts").ToMember();
            IListOfDoubles = typeof(Target).GetProperty("IListOfDoubles").ToMember();
            IListOfShorts = typeof(Target).GetProperty("IListOfShorts").ToMember();
            IListOfLongs = typeof(Target).GetProperty("IListOfLongs").ToMember();
            IListOfFloats = typeof(Target).GetProperty("IListOfFloats").ToMember();
            IListOfBools = typeof(Target).GetProperty("IListOfBools").ToMember();
            IListOfDateTimes = typeof(Target).GetProperty("IListOfDateTimes").ToMember();
            IListOfTargets = typeof(Target).GetProperty("IListOfTargets").ToMember();
            ISetOfTargets = typeof(Target).GetProperty("ISetOfTargets").ToMember();
            String = typeof(Target).GetProperty("String").ToMember();
            Entity = typeof(Target).GetProperty("Entity").ToMember();
        }

        class Target
        {
            public IList<string> IListOfStrings { get; set; }
            public IList<int> IListOfInts { get; set; }
            public IList<double> IListOfDoubles { get; set; }
            public IList<short> IListOfShorts { get; set; }
            public IList<long> IListOfLongs { get; set; }
            public IList<float> IListOfFloats { get; set; }
            public IList<bool> IListOfBools { get; set; }
            public IList<DateTime> IListOfDateTimes { get; set; }
            public IList<Target> IListOfTargets { get; set; }
            public ISet<Target> ISetOfTargets { get; set; }
            public string String { get; set; }
            public Target Entity { get; set; }
        }
    }
}