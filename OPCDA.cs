﻿using OPCAutomation;
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
        static string ServerName = "Owen.OPCNet.DA.1";
        static OPCServer _OPCServer = new OPCServer();
        static OPCGroup _OPCGroup = null;

        public static List<Tag> AllTags = new List<Tag>()
        {
            //new Tag("C_CurrentWeight", "Узел1.PLC110.Весы С.Тег1"),
        };

        static OPCDA()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\OPCDAAuto.dll"))
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "RegOPCDAAuto\\InstallOPCDAAuto.exe");
        }

        public static void ConnectionOPC()
        {
            try
            {
                _OPCServer.Connect(ServerName, "localhost");
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

            foreach (Tag item in AllTags)
                _OPCGroup.OPCItems.AddItem(item.Path, item.ClientHandle);
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
