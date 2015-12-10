using System;
using System.ComponentModel;

namespace HopeHouse.Core.Models
{
    public class ExternalProgram
    {
        private int _externalprogramid;
        private string _externalprogramname;
        private string _externalprogramdescription;
        private DateTime _starttime;
        private DateTime _endtime;

        [Description("External Program ID")]
        public int ExternalProgramId
        {
            get { return _externalprogramid; }
            set { _externalprogramid = value; }
        }

        [Description("External Program Name")]
        public string ExternalProgramName
        {
            get { return _externalprogramname; }
            set { _externalprogramname = value; }
        }

        [Description("External Program Description")]
        public string ExternalProgramDescription
        {
            get { return _externalprogramdescription; }
            set { _externalprogramdescription = value; }
        }

        [Description("External Program Start Time")]
        public DateTime StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        [Description("External Program End Time")]
        public DateTime EndTime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }


        public override string ToString()
        {
            return _externalprogramname;
        }
    }
}