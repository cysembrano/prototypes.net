/**
 * BUILDER
 * From Wikipedia
 * The intent of the Builder design pattern is to separate the construction of a complex object from its representation. 
 * By doing so the same construction process can create different representations. [1]
 * 
 * USES:
 * Constructing objects with different property values.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Builder
{
    class Client
    {
        static void Main(string[] args)
        {
            var mydirector = new Director(new MyBuilder());
            mydirector.Build();
            Console.WriteLine(mydirector.GetProduct().Display());
            Console.WriteLine();

            var hisdirector = new Director(new HisBuilder());
            hisdirector.Build();
            Console.WriteLine(hisdirector.GetProduct().Display());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Builder only works with 1 product.
    /// </summary>
    public class Director
    {
        private readonly BuilderBase _builder;
        public Director(BuilderBase builder)
        {
            this._builder = builder;
        }

        public void Build()
        {
            this._builder.Create();
            this._builder.BuildStepOne();
            this._builder.BuildStepTwo();
            this._builder.BuildStepThree();
        }

        public Product GetProduct()
        {
            return this._builder.GetProduct();
        }
        
    }

    /// <summary>
    /// Abstract Builder Base Class
    /// </summary>
    public abstract class BuilderBase
    {
        protected Product _product;
        public Product GetProduct()
        {
            return _product;
        }

        public void Create()
        {
            _product = new Product();
        }

        public abstract void BuildStepOne();

        public abstract void BuildStepTwo();

        public abstract void BuildStepThree();

    }

    /// <summary>
    /// Concreate Builder
    /// </summary>
    public class MyBuilder : BuilderBase
    {
        
        public override void BuildStepOne()
        {

            _product.PropertyOne = "MY StepOne";
        }

        public override void BuildStepTwo()
        {
            _product.PropertyTwo = "MY StepTwo";
        }

        public override void BuildStepThree()
        {
            _product.PropertyThree = "MY StepThree";
        }
    }

    /// <summary>
    /// Concreate Builder
    /// </summary>
    public class HisBuilder : BuilderBase
    {
        public override void BuildStepOne()
        {
            _product.PropertyOne = "HIS StepOne";
        }

        public override void BuildStepTwo()
        {
            _product.PropertyTwo = "HIS StepTwo";
        }

        public override void BuildStepThree()
        {
            _product.PropertyThree = "HIS StepThree";
        }
    }

    public class Product
    {
        public string PropertyOne { get; set; }
        public string PropertyTwo { get; set; }
        public string PropertyThree { get; set; }
        public string Display()
        {
            return string.Format("1:{0}, 2:{1}, 3:{2}", PropertyOne, PropertyTwo, PropertyThree);
        }
    }



}
