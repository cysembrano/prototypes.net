/**
 * From Wikipedia
 * The intent of the Builder design pattern is to separate the construction of a complex object from its representation. 
 * By doing so the same construction process can create different representations. [1]
 * 
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
            var director = new Director(new MyBuilder());
            director.Build();
            director.GetProduct().Display();
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
        private Product _product;
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
            throw new NotImplementedException();
        }

        public override void BuildStepTwo()
        {
            throw new NotImplementedException();
        }

        public override void BuildStepThree()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Concreate Builder
    /// </summary>
    public class HisBuilder : BuilderBase
    {
        public override void BuildStepOne()
        {
            throw new NotImplementedException();
        }

        public override void BuildStepTwo()
        {
            throw new NotImplementedException();
        }

        public override void BuildStepThree()
        {
            throw new NotImplementedException();
        }
    }

    public class Product
    {
        public int MyProperty { get; set; }
        public string Display()
        {
            return "Test";
        }
    }



}
