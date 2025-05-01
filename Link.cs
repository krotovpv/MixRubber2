using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MixRubber2
{
    internal static class Link //: INotifyPropertyChanged
    {
        internal static int PingPeriod { get; set; } = 4000;

        internal static IPAddress ipScalesC { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesJ { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesD { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesE { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesSh { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesJu { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesY { get; } = IPAddress.Parse("10.0.6.18");
        internal static IPAddress ipScalesK { get; } = IPAddress.Parse("10.0.6.18");

        public static event PropertyChangedEventHandler PropertyChanged;
        internal static void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        static IPStatus scalesC = IPStatus.Unknown;
        internal static IPStatus ScalesC
        {
            get { return scalesC; }
            set
            {
                if (value != scalesC)
                {
                    scalesC = value;
                    ScalesCLinkChanged?.Invoke(scalesC);
                }
            }
        }
        internal static event Action<IPStatus> ScalesCLinkChanged;

        static IPStatus scalesJ = IPStatus.Unknown;
        internal static IPStatus ScalesJ
        {
            get { return scalesJ; }
            set
            {
                if (value != scalesJ)
                {
                    scalesJ = value;
                    ScalesJLinkChanged?.Invoke(scalesJ);
                }
            }
        }
        internal static event Action<IPStatus> ScalesJLinkChanged;

        static IPStatus scalesD = IPStatus.Unknown;
        internal static IPStatus ScalesD
        {
            get { return scalesD; }
            set
            {
                if (value != scalesD)
                {
                    scalesD = value;
                    ScalesDLinkChanged?.Invoke(scalesD);
                }
            }
        }
        internal static event Action<IPStatus> ScalesDLinkChanged;

        static IPStatus scalesE = IPStatus.Unknown;
        internal static IPStatus ScalesE
        {
            get { return scalesE; }
            set
            {
                if (value != scalesE)
                {
                    scalesE = value;
                    ScalesELinkChanged?.Invoke(scalesE);
                }
            }
        }
        internal static event Action<IPStatus> ScalesELinkChanged;

        static IPStatus scalesSh = IPStatus.Unknown;
        internal static IPStatus ScalesSh
        {
            get { return scalesSh; }
            set
            {
                if (value != scalesSh)
                {
                    scalesSh = value;
                    ScalesShLinkChanged?.Invoke(scalesSh);
                }
            }
        }
        internal static event Action<IPStatus> ScalesShLinkChanged;

        static IPStatus scalesJu = IPStatus.Unknown;
        internal static IPStatus ScalesJu
        {
            get { return scalesJu; }
            set
            {
                if (value != scalesJu)
                {
                    scalesJu = value;
                    ScalesJuLinkChanged?.Invoke(scalesJu);
                }
            }
        }
        internal static event Action<IPStatus> ScalesJuLinkChanged;

        static IPStatus scalesY = IPStatus.Unknown;
        internal static IPStatus ScalesY
        {
            get { return scalesY; }
            set
            {
                if (value != scalesY)
                {
                    scalesY = value;
                    ScalesYLinkChanged?.Invoke(scalesY);
                }
            }
        }
        internal static event Action<IPStatus> ScalesYLinkChanged;

        static IPStatus scalesK = IPStatus.Unknown;
        internal static IPStatus ScalesK
        {
            get { return scalesK; }
            set
            {
                if (value != scalesK)
                {
                    scalesK = value;
                    ScalesKLinkChanged?.Invoke(scalesK);
                }
            }
        }
        internal static event Action<IPStatus> ScalesKLinkChanged;

        static Link()
        {

        }

    }
}
