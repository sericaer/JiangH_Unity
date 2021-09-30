using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class Facade
    {
        public static RunData runData { get; private set; }

        public static IPerson player { get; private set; }

        public static IEnumerable<IBranch> branchs { get; private set; }

        public static IEnumerable<IPerson> persons { get; private set; }

        public static IDate date { get; private set; }

        public static SystemManager system { get; private set; }

        public static void BuildRunData()
        {
            runData = new RunData();

            persons = runData.persons;

            date = runData.date;

            system = runData.systemMgr;

            branchs = runData.branches;

            player = persons.First();
        }

        public static IBranch CreateBranch(string name, out string erroMsg)
        {
            erroMsg = "";

            if (runData == null)
            {
                throw new Exception();
            }

            if(runData.branches.Any(x=>x.name == name))
            {
                erroMsg = $"已经存在名字为{name}的堂口";
                return null;
            }

            var branch = Branch.Create(name);
            runData.entityMgr.AddEntity(branch);
            return branch;
        }


    }
}
