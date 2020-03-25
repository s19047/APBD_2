using Apbd_Tutorial2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Apbd_Tutorial2
{
    class customComparer : IEqualityComparer<Student>, IEqualityComparer<Study>, IEqualityComparer<activeStudy>
    {
        public bool Equals( Student x,  Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.indexNumber}{x.fName}{x.lName}{x.BirthDate}{x.Email}{x.Mother}{x.Father}",
                                                                    $"{x.indexNumber}{x.fName}{x.lName}{x.BirthDate}{x.Email}{x.Mother}{x.Father}");
        }

        public bool Equals( Study x, Study y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.name}{x.mode}", $"{y.name}{y.mode}");
        }

        public bool Equals( activeStudy x,  activeStudy y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.name}{x.numOfStudents}", $"{y.name}{y.numOfStudents}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode($"{obj.indexNumber}{obj.fName}{obj.lName}{obj.BirthDate}" +
                                                                          $"{obj.Email}{obj.Mother}{obj.Father}");
        }

        public int GetHashCode(Study obj)
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode($"{obj.name}{obj.mode}");
        }

        public int GetHashCode([DisallowNull] activeStudy obj)
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode($"{obj.name}{obj.numOfStudents}");
        }
    }
}
