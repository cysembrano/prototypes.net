/**
 * From Gang of four
 * Compose objects into tree structures to represent part-whole heirarchies.
 * Composite lets clients treat individual objects and compositions of object uniformly. (GOF)
 * 
 * Uses:
 * Windows explorer deletes folders in the same way it deletes a file.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    //The Interface
    public interface IComponent<T>
    {
        void Add(IComponent<T> c);
        IComponent<T> Remove(T s);
        string Display(int depth);
        IComponent<T> Find(T s);
        T Name { get; set; }
    }

    //The Component
    public class Component<T> : IComponent<T>
    {
        public T Name { get; set; }

        public Component(T name)
        {
            Name = name;
        }

        public void Add(IComponent<T> c)
        {
            throw new Exception("Cannot add to an item.");
        }

        public IComponent<T> Remove(T s)
        {
            throw new Exception("Cannot remove directly.");
        }

        public string Display(int depth)
        {
            return new String('-', depth) + Name + "\n";
        }

        public IComponent<T> Find(T s)
        {
            if (s.Equals(Name))
                return this;
            else
                return null;
        }

        
    }

    public class Composite<T> : IComponent<T>
    {
        List<IComponent<T>> list;
        public T Name { get; set; }

        public Composite(T name)
        {
            this.Name = name;
            this.list = new List<IComponent<T>>();
        }


        public void Add(IComponent<T> c)
        {
            list.Add(c);
        }

        IComponent<T> holder = null;
        public IComponent<T> Remove(T s)
        {
            holder = this;
            IComponent<T> p = holder.Find(s);
            if (holder != null)
            {
                (holder as Composite<T>).list.Remove(p);
                return holder;
            }
            else
                return this;              
        }

        public string Display(int depth)
        {
            StringBuilder s = new StringBuilder(new String('-', depth));
            s.Append("Set " + Name + " length:" + list.Count + "\n");
            foreach (IComponent<T> component in list)
            {
                s.Append(component.Display(depth + 2));
            }
            return s.ToString();
        }

        public IComponent<T> Find(T s)
        {
            holder = this;
            if (Name.Equals(s)) return this;
            IComponent<T> found = null;
            foreach (IComponent<T> c in list)
            {
                found = c.Find(s);
                if (found != null)
                    break;

            }
            return found;
        }

        
    }

}
