using System;

namespace ObserverPattern
{
    class Program
    {
        /// <summary>
        /// 观察者模式例子
        /// 背景：猫大叫,两只老鼠开始逃跑,主人醒来,宝宝也醒来了并且哭了起来
        /// </summary>
        /// <param name="args"></param>
        static void Main (string[] args)
        {
            // 定义猫
            var myCat = new Cat();

            // 定义两只老鼠
            var myMouse1 = new Mouse("mouse1", myCat);
            var myMouse2 = new Mouse("mouse2", myCat);

            // 定义主人
            var myMaster = new Master(myCat);

            // 定义baby
            var myBaby = new Baby(myCat);

            // 猫大叫
            myCat.Cry();

            Console.Read();
        }
    }

    public class SubjectBase
    {
        public delegate void SubEventHandler ();

        public event SubEventHandler SubEvent;

        protected void Notify ()
        {
            SubEvent?.Invoke();
        }
    }

    public abstract class ObserverSingle
    {
        protected ObserverSingle (SubjectBase subject)
        {
            subject.SubEvent += new SubjectBase.SubEventHandler(Response);
        }

        public abstract void Response ();
    }

    public abstract class ObserverMultiple
    {
        protected ObserverMultiple (SubjectBase subject)
        {
            subject.SubEvent += new SubjectBase.SubEventHandler(Response);
            subject.SubEvent += new SubjectBase.SubEventHandler(Response2);
        }

        public abstract void Response ();

        public abstract void Response2 ();
    }

    public class Cat : SubjectBase
    {
        public void Cry ()
        {
            Console.WriteLine("Cat Cry..");

            this.Notify();
        }
    }

    public class Mouse : ObserverSingle
    {
        private readonly string _name;

        public Mouse (string name, SubjectBase subject) : base(subject)
        {
            this._name = name;
        }

        public override void Response ()
        {
            Console.WriteLine($"{this._name}开始逃跑");
        }
    }

    public class Master : ObserverSingle
    {
        public Master (SubjectBase subject) : base(subject)
        {

        }

        public override void Response ()
        {
            Console.WriteLine("主人醒来");
        }
    }

    public class Baby : ObserverMultiple
    {
        public Baby (SubjectBase subject) : base(subject)
        {

        }

        public override void Response ()
        {
            Console.WriteLine("Baby 醒来");
        }

        public override void Response2 ()
        {
            Console.WriteLine("Baby 开始哭闹");
        }
    }
}
