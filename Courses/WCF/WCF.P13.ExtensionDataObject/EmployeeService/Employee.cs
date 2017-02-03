using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EmployeeService
{
    public enum EmployeeType
    {
        FullTimeEmployee = 1,
        PartTimeEmployee = 2
    }

    /// <summary>
    /// http://localhost:8080/?xsd=xsd2
    /// You'll find the serialization on the above url.  All marked with DataMember will be serialized on this complex type.
    /// Decorate the type with DataContract and members with DataMember.
    /// 
    /// You can also specify the namespace on the DataContract Attribute.
    /// 
    /// You can specify the Name & Order on the DataMembers.
    /// 
    /// You can also use [Serializable] but this has less control as it will also serialize your private fields.
    /// </summary>
    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]
    [DataContract(Namespace = "http://cymessageboards.com/2015/10/06/Employee")]
    public class Employee
    {
        private int _id;
        private string _name;
        private string _gender;
        private DateTime _dateOfBirth;

        //Properties come out in alphabetical order, Use "Order" property to control order.
        [DataMember(Name = "ID", Order = 1)] 
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember(Order = 2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember(Order = 3)]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember(Order = 4)]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        [DataMember(Order = 5)]
        public EmployeeType Type { get; set; }

        [DataMember(Order = 6)]
        public string City { get; set; }

    }
}
