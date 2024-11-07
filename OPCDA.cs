using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace MixRubber2
{
    public static class OPCDA
    {
        static bool IsAlternativeOPC = false;
        static string ServerName = "Owen.OPCNet.DA.1";
        static string AlernativeServerName = "";
        static OPCServer _OPCServer = new OPCServer();
        static OPCGroup _OPCGroup = null;

        public static List<Tag> AllTags = new List<Tag>()
        {
           //lkl;kl;k
            new Tag("BatcherC1", "MixRubber17.PLC110-504.BatchersC.BatcherC1"),
            new Tag("BatcherC2", "MixRubber17.PLC110-504.BatchersC.BatcherC2"),
            new Tag("BatcherC3", "MixRubber17.PLC110-504.BatchersC.BatcherC3"),
            new Tag("BatcherC4", "MixRubber17.PLC110-504.BatchersC.BatcherC4"),
            new Tag("BatcherC5", "MixRubber17.PLC110-504.BatchersC.BatcherC5"),

            new Tag("ScalesC_IsClosed", "MixRubber17.PLC110-504.ScalesC_Status.IsClosed"),
            new Tag("ScalesC_IsOpened", "MixRubber17.PLC110-504.ScalesC_Status.IsOpened"),
            new Tag("ScalesC_Weighing", "MixRubber17.PLC110-504.ScalesC_Status.Weighing"),

            new Tag("ScalesC.CurNeed", "MixRubber17.PLC100_ScalesC.CurNeed"),
            new Tag("ScalesC.FullNeed", "MixRubber17.PLC100_ScalesC.FullNeed"),
            new Tag("ScalesC.CurReal", "MixRubber17.PLC100_ScalesC.CurReal"),
            new Tag("ScalesC.FullReal", "MixRubber17.PLC100_ScalesC.FullReal"),

            new Tag("BatcherJ1", "MixRubber17.PLC110-504.BatchersJ.BatcherJ1"),
            new Tag("BatcherJ2", "MixRubber17.PLC110-504.BatchersJ.BatcherJ2"),
            new Tag("BatcherJ3", "MixRubber17.PLC110-504.BatchersJ.BatcherJ3"),
            new Tag("BatcherJ4", "MixRubber17.PLC110-504.BatchersJ.BatcherJ4"),

            new Tag("ScalesJ_IsClosed", "MixRubber17.PLC110-504.ScalesJ_Status.IsClosed"),
            new Tag("ScalesJ_Weighing", "MixRubber17.PLC110-504.ScalesJ_Status.Weighing"),

            new Tag("ScalesJ_CurNeed", "MixRubber17.PLC100_ScalesJ.CurNeed"),
            new Tag("ScalesJ_FullNeed", "MixRubber17.PLC100_ScalesJ.FullNeed"),
            new Tag("ScalesJ_CurReal", "MixRubber17.PLC100_ScalesJ.CurReal"),
            new Tag("ScalesJ_FullReal", "MixRubber17.PLC100_ScalesJ.FullReal"),
            
            new Tag("BatcherD1", "MixRubber17.PLC110-504.BatchersD.BatcherD1"),
            new Tag("BatcherD2", "MixRubber17.PLC110-504.BatchersD.BatcherD2"),
            new Tag("BatcherD3", "MixRubber17.PLC110-504.BatchersD.BatcherD3"),
            new Tag("BatcherD4", "MixRubber17.PLC110-504.BatchersD.BatcherD4"),

            new Tag("ScalesD_IsClosed", "MixRubber17.PLC110-504.ScalesD_Status.IsClosed"),
            new Tag("ScalesD_Weighing", "MixRubber17.PLC110-504.ScalesD_Status.Weighing"),

            new Tag("ScalesD_CurNeed", "MixRubber17.PLC100_ScalesD.CurNeed"),
            new Tag("ScalesD_FullNeed", "MixRubber17.PLC100_ScalesD.FullNeed"),
            new Tag("ScalesD_CurReal", "MixRubber17.PLC100_ScalesD.CurReal"),
            new Tag("ScalesD_FullReal", "MixRubber17.PLC100_ScalesD.FullReal"),
            
            new Tag("BatcherE1", "MixRubber17.PLC110-504.BatchersE.BatcherE1"),
            new Tag("BatcherE2", "MixRubber17.PLC110-504.BatchersE.BatcherE2"),
            new Tag("BatcherE3", "MixRubber17.PLC110-504.BatchersE.BatcherE3"),

            new Tag("ScalesE_IsClosed", "MixRubber17.PLC110-504.ScalesE_Status.IsClosed"),
            new Tag("ScalesE_Weighing", "MixRubber17.PLC110-504.ScalesE_Status.Weighing"),

            new Tag("ScalesE_CurNeed", "MixRubber17.PLC100_ScalesE.CurNeed"),
            new Tag("ScalesE_FullNeed", "MixRubber17.PLC100_ScalesE.FullNeed"),
            new Tag("ScalesE_CurReal", "MixRubber17.PLC100_ScalesE.CurReal"),
            new Tag("ScalesE_FullReal", "MixRubber17.PLC100_ScalesE.FullReal"),
            
            new Tag("BatcherSh1", "MixRubber17.PLC110-507.BatchersSh.BatcherSh1"),
            new Tag("BatcherSh2", "MixRubber17.PLC110-507.BatchersSh.BatcherSh2"),
            new Tag("BatcherSh3", "MixRubber17.PLC110-507.BatchersSh.BatcherSh3"),

            new Tag("ScalesSh_IsClosed", "MixRubber17.PLC110-507.ScalesSh_Status.IsClosed"),
            new Tag("ScalesSh_Weighing", "MixRubber17.PLC110-504.ScalesSh_Status.Weighing"),

            new Tag("ScalesSh_CurNeed", "MixRubber17.PLC100_ScalesSh.CurNeed"),
            new Tag("ScalesSh_FullNeed", "MixRubber17.PLC100_ScalesSh.FullNeed"),
            new Tag("ScalesSh_CurReal", "MixRubber17.PLC100_ScalesSh.CurReal"),
            new Tag("ScalesSh_FullReal", "MixRubber17.PLC100_ScalesSh.FullReal"),
            
            new Tag("BatcherJu1", "MixRubber17.PLC110-507.BatchersJu.BatcherJu1"),
            new Tag("BatcherJu2", "MixRubber17.PLC110-507.BatchersJu.BatcherJu2"),
            new Tag("BatcherJu3", "MixRubber17.PLC110-507.BatchersJu.BatcherJu3"),

            new Tag("ScalesJu_IsClosed", "MixRubber17.PLC110-507.ScalesJu_Status.IsClosed"),
            new Tag("ScalesJu_Weighing", "MixRubber17.PLC110-504.ScalesJu_Status.Weighing"),

            new Tag("ScalesJu_CurNeed", "MixRubber17.PLC100_ScalesJu.CurNeed"),
            new Tag("ScalesJu_FullNeed", "MixRubber17.PLC100_ScalesJu.FullNeed"),
            new Tag("ScalesJu_CurReal", "MixRubber17.PLC100_ScalesJu.CurReal"),
            new Tag("ScalesJu_FullReal", "MixRubber17.PLC100_ScalesJu.FullReal"),
            
            new Tag("BatcherY1", "MixRubber17.PLC110-507.BatchersY.BatcherY1"),
            new Tag("BatcherY2", "MixRubber17.PLC110-507.BatchersY.BatcherY2"),
            new Tag("BatcherY3", "MixRubber17.PLC110-507.BatchersY.BatcherY3"),

            new Tag("ScalesY_IsClosed", "MixRubber17.PLC110-507.ScalesY_Status.IsClosed"),
            new Tag("ScalesY_Weighing", "MixRubber17.PLC110-504.ScalesY_Status.Weighing"),

            new Tag("ScalesY_CurNeed", "MixRubber17.PLC100_ScalesY.CurNeed"),
            new Tag("ScalesY_FullNeed", "MixRubber17.PLC100_ScalesY.FullNeed"),
            new Tag("ScalesY_CurReal", "MixRubber17.PLC100_ScalesY.CurReal"),
            new Tag("ScalesY_FullReal", "MixRubber17.PLC100_ScalesY.FullReal"),
        };

        static OPCDA()
        {
            //if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\OPCDAAuto.dll"))
            //    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "RegOPCDAAuto\\InstallOPCDAAuto.exe");
        }

        public static void ConnectionOPC()
        {
            try
            {
                _OPCServer.Connect(IsAlternativeOPC ? AlernativeServerName : ServerName, "localhost");
                _OPCGroup = _OPCServer.OPCGroups.Add("SCADAGroup");
                _OPCGroup.IsActive = true;
                _OPCGroup.IsSubscribed = true;
                _OPCGroup.UpdateRate = 1000;    //1 second
                _OPCGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(OPCGroupDataChange);

                //_OPCGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(OPCGroupAsyncWriteComplete);
                //_OPCGroup.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(OPCGroupAsyncReadComplete);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Connect!");
            }

            AddOPCItems();
        }

        static void AddOPCItems()
        {
            if (IsAlternativeOPC)
            {
                foreach (Tag item in AllTags)
                    _OPCGroup.OPCItems.AddItem(item.AlternativePath, item.ClientHandle);
            }
            else
            {
                foreach (Tag item in AllTags)
                    _OPCGroup.OPCItems.AddItem(item.Path, item.ClientHandle);
            }
        }

        public static void DisconnectOPC()
        {
            if (_OPCServer != null)
                _OPCServer.Disconnect();
        }

        /// <summary>
        /// Данные тэгов изменены
        /// </summary>
        /// <param name="TransactionID">Идентификатор операции</param>
        /// <param name="NumItems">Количество тегов с измененными данными</param>
        /// <param name="ClientHandles">Идентификаторы тэгов заданные клиентом</param>
        /// <param name="ItemValues">Значение тэгов</param>
        /// <param name="Qualities">Качество тэгов</param>
        /// <param name="TimeStamps">Время получение данных</param>
        private static void OPCGroupDataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            //Все массивы начинаются с 1!!!
            for (int i = 1; i < NumItems + 1; i++)
            {
                int clientHandel = (int)ClientHandles.GetValue(i);
                Tag tag = AllTags.Where(x => x.ClientHandle == clientHandel).First();
                if (tag != null)
                {
                    tag.Value = ItemValues.GetValue(i);
                    tag.Qualitie = (OPCQuality)Qualities.GetValue(i);
                    tag.TimeStamp = Convert.ToDateTime(TimeStamps.GetValue(i));
                }
            }
        }
    }
}
