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
        static bool IsAlternativeOPC = true;
        static string ServerName = "Owen.OPCNet.DA.1";
        static string AlernativeServerName = "Lectus.OPC.1";
        static OPCServer _OPCServer = new OPCServer();
        static OPCGroup _OPCGroup = null;

        public static List<Tag> AllTags = new List<Tag>()
        {
            #region Scales C
            new Tag("BatcherC1", "MixRubber17.PLC110-504.BatchersC.BatcherC1", "plc110.plc110_SCADA.Inp_48", 0),
            new Tag("BatcherC2", "MixRubber17.PLC110-504.BatchersC.BatcherC2", "plc110.plc110_SCADA.Inp_48", 1),
            new Tag("BatcherC3", "MixRubber17.PLC110-504.BatchersC.BatcherC3", "plc110.plc110_SCADA.Inp_48", 2),
            new Tag("BatcherC4", "MixRubber17.PLC110-504.BatchersC.BatcherC4", "plc110.plc110_SCADA.Inp_48", 3),
            new Tag("BatcherC5", "MixRubber17.PLC110-504.BatchersC.BatcherC5", "plc110.plc110_SCADA.Inp_48", 4),
        
            new Tag("ScalesC_IsClosed", "MixRubber17.PLC110-504.ScalesC_Status.IsClosed","plc110.plc110_SCADA.Inp_32", 0),
            new Tag("ScalesC_IsOpened", "MixRubber17.PLC110-504.ScalesC_Status.IsOpened","plc110.plc110_SCADA.Inp_32",1),
            new Tag("ScalesC_Weighing", "MixRubber17.PLC110-504.ScalesC_Status.Weighing","plc110.plc110_SCADA.B19", 2),
            new Tag("ScalesC_IsBusy", "MixRubber17.PLC110-504.ScalesC_Status.Tare_IsBusy", "plc110.plc110_SCADA.Inp_32", 2),//!
            new Tag("ScalesC_WeightNotNormal", "MixRubber17.PLC110-504.ScalesC_Status.Weight_NotNormal", "plc110.plc110_SCADA.Inp_32", 3),//!
            new Tag("ScalesC_Initial", "MixRubber17.PLC110-504.ScalesC_Status.Initial", "plc110.plc110_SCADA.Inp_32", 5), //!
            // new Tag("ScalesC_CommandOpen", "MixRubber17.PLC110-504.ScalesC_Status.CommandOpen","plc110.plc110_SCADA.B7", 10),

            // new Tag("ScalesC_IsNotOpening", "MixRubber17.PLC110-504.ScalesC_Fail.IsNotOpening", "plc110.plc110_SCADA.AV1", 0),
            // new Tag("ScalesC_IsNotClosing", "MixRubber17.PLC110-504.ScalesC_Fail.IsNotClosing", "plc110.plc110_SCADA.AV1", 1),
            // new Tag("ScalesC_BatcherFail", "MixRubber17.PLC110-504.ScalesC_Fail.BatcherFail", "plc110.plc110_SCADA.AV1", 2),
            // new Tag("ScalesC_BatcherNotWork", "MixRubber17.PLC110-504.ScalesC_Fail.Batcher_NotWork", "plc110.plc110_SCADA.AV4", 12),
            // new Tag("ScalesC_ClosedAndOpened", "MixRubber17.PLC110-504.ScalesC_Fail.ClosedAndOpened", "plc110.plc110_SCADA.AV1", 3),

            new Tag("ScalesC_CurNeed", "MixRubber17.PLC100_ScalesC.CurNeed", "VesiC.CurNeed"),
            new Tag("ScalesC_FullNeed", "MixRubber17.PLC100_ScalesC.FullNeed", "VesiC.FullNeed"),
            // new Tag("ScalesC_Overweight", "MixRubber17.PLC100_ScalesC.Overweight", "VesiC.Pereves"), //Весы С.Вес не норма
            new Tag("ScalesC_CurReal", "MixRubber17.PLC100_ScalesC.CurReal", "VesiC.FullReal"), // Бункер.Измеренный вес
            new Tag("ScalesC_FullReal", "MixRubber17.PLC100_ScalesC.FullReal", "VesiC.FullReal"), //Весы С.Данные.Общий измеренный вес
            #endregion

            #region Conteiner C
            new Tag("ContainerC_IsClosed", "MixRubber17.PLC110-504.ContainerC_Status.IsClosed", "plc110.plc110_SCADA.Inp_32", 7), //!
            new Tag("ContainerC_IsOpened", "MixRubber17.PLC110-504.ContainerC_Status.IsOpened", "plc110.plc110_SCADA.Inp_32", 15), //!
            // new Tag("ContainerC_BatcherNum", "MixRubber17.PLC110-504.ContainerC_Status.BatcherNum", "plc110.plc110_SCADA.CV0_na_C"),
            // new Tag("ContainerC_CommandOpen", "MixRubber17.PLC110-504.ContainerC_Status.CommandOpen", "plc110.plc110_SCADA.B7", 14),
            new Tag("ContainerC_Weighing", "MixRubber17.PLC110-504.ContainerC_Status.Weighing", "plc110.plc110_SCADA.B7", 4),

            // new Tag("ContainerC_IsNotClosing", "MixRubber17.PLC110-504.ContainerC_Fail.IsNotClosing","plc110.plc110_SCADA.AV2", 0),
            // new Tag("ContainerC_IsNotOpening", "MixRubber17.PLC110-504.ContainerC_Fail.IsNotOpening","plc110.plc110_SCADA.AV1", 1),
            // new Tag("ContainerC_ClosedAndOpened", "MixRubber17.PLC110-504.ContainerC_Fail.ClosedAndOpened", "plc110.plc110_SCADA.AV1", 2),
            // new Tag("ContainerC_NotFull", "MixRubber17.PLC110-504.ContainerC_Fail.NotFull", "plc110.plc110_SCADA.AV1", 3),

            new Tag("ScrewTC_Working", "MixRubber17.PLC110-504.ScrewTC.Working", "plc110.plc110_SCADA.B8", 12),
            // new Tag("ScrewTC_CommandOn", "MixRubber17.PLC110-504.ScrewTC.CommandOn", "plc110.plc110_SCADA.B19", 9),
            // new Tag("ScrewTC_NotOn", "MixRubber17.PLC110-504.ScrewTC.NotOn", plc110.plc110_SCADA.AV3", 3),
            // new Tag("ScrewTC_NotStoped", "MixRubber17.PLC110-504.ScrewTC.NotStoped", "plc110.plc110_SCADA.AV3", 4),
            #endregion

            #region Scales J
            new Tag("BatcherJ1", "MixRubber17.PLC110-504.BatchersJ.BatcherJ1", "plc110.plc110_SCADA.Inp_48", 5),
            new Tag("BatcherJ2", "MixRubber17.PLC110-504.BatchersJ.BatcherJ2", "plc110.plc110_SCADA.Inp_48", 6),
            new Tag("BatcherJ3", "MixRubber17.PLC110-504.BatchersJ.BatcherJ3", "plc110.plc110_SCADA.Inp_48", 7),
            new Tag("BatcherJ4", "MixRubber17.PLC110-504.BatchersJ.BatcherJ4", "plc110.plc110_SCADA.Inp_48", 8),

            new Tag("ScalesJ_IsClosed", "MixRubber17.PLC110-504.ScalesJ_Status.IsClosed", "plc110.plc110_SCADA.Inp_32", 8),
            new Tag("ScalesJ_Weighing", "MixRubber17.PLC110-504.ScalesJ_Status.Weighing", "plc110.plc110_SCADA.B19", 5),
            new Tag("ScalesJ_IsBusy", "MixRubber17.PLC110-504.ScalesJ_Status.Tare_IsBusy", "plc110.plc110_SCADA.Inp_32", 10),//!
            new Tag("ScalesJ_WeightNotNormal", "MixRubber17.PLC110-504.ScalesJ_Status.Weight_NotNormal", "plc110.plc110_SCADA.Inp_32", 12),//!
            new Tag("ScalesJ_Initial", "MixRubber17.PLC110-504.ScalesJ_Status.Initial", "plc110.plc110_SCADA.Inp_32", 13),//!
            //new Tag("ScalesJ_CommandOpen", "MixRubber17.PLC110-504.ScalesJ_Status.CommandOpen", "plc110.plc110_SCADA.B7", 11),

            //new Tag("ScalesJ_BatcherNotWork", "MixRubber17.PLC110-504.ScalesJ_Fail.Batcher_NotWork", "plc110.plc110_SCADA.AV4", 13),
            //new Tag("ScalesJ_IsNotOpening", "MixRubber17.PLC110-504.ScalesJ_Fail.IsNotOpening", "plc110.plc110_SCADA.AV1", 4),
            //new Tag("ScalesJ_IsNotClosing", "MixRubber17.PLC110-504.ScalesJ_Fail.IsNotClosing", "plc110.plc110_SCADA.AV1", 5),
            //new Tag("ScalesJ_BatcherFail", "MixRubber17.PLC110-504.ScalesJ_Fail.BatcherFail", "plc110.plc110_SCADA.AV1", 6),

            new Tag("ScalesJ_CurNeed", "MixRubber17.PLC100_ScalesJ.CurNeed", "VesiJ.CurNeed"), // Бункер.Заданный вес
            new Tag("ScalesJ_FullNeed", "MixRubber17.PLC100_ScalesJ.FullNeed", "VesiJ.FullNeed"), //Весы Ж.Общий заданный вес
            // new Tag("ScalesJ_Overweight", "MixRubber17.PLC100_ScalesJ.Overweight", "VesiJ.Pereves"), //Весы Ж.Вес не норма
            new Tag("ScalesJ_CurReal", "MixRubber17.PLC100_ScalesJ.CurReal", "VesiJ.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesJ_FullReal", "MixRubber17.PLC100_ScalesJ.FullReal", "VesiJ.FullReal"), //Весы Ж.Общий измеренный вес
            #endregion

            #region Injector
            new Tag("Injector_Weighing", "MixRubber17.PLC110-504.Injector.Weighing", "plc110.plc110_SCADA.B8",6),//емкость для жидкости наполнена
            //new Tag("Injector_NotEmpty", "MixRubber17.PLC110-504.Injector.NotEmpty", "plc110.plc110_SCADA.AV2",12),
            //new Tag("Injector_NotFull", "MixRubber17.PLC110-504.Injector.NotFull", "plc110.plc110_SCADA.AV",13),
            #endregion

            #region Scales D
            new Tag("BatcherD1", "MixRubber17.PLC110-504.BatchersD.BatcherD1", "plc110.plc110_SCADA.Inp_48", 9),
            new Tag("BatcherD2", "MixRubber17.PLC110-504.BatchersD.BatcherD2", "plc110.plc110_SCADA.Inp_48", 10),
            new Tag("BatcherD3", "MixRubber17.PLC110-504.BatchersD.BatcherD3", "plc110.plc110_SCADA.Inp_48", 11),
            new Tag("BatcherD4", "MixRubber17.PLC110-504.BatchersD.BatcherD4", "plc110.plc110_SCADA.Inp_48", 12),

            new Tag("ScalesD_IsClosed", "MixRubber17.PLC110-504.ScalesD_Status.IsClosed", "plc110.plc110_SCADA.Inp_40", 0),
            new Tag("ScalesD_Weighing", "MixRubber17.PLC110-504.ScalesD_Status.Weighing", "plc110.plc110_SCADA.B19", 6),
            new Tag("ScalesD_IsBusy", "MixRubber17.PLC110-504.ScalesD_Status.Tare_IsBusy", "plc110.plc110_SCADA.Inp_40", 2),//!
            new Tag("ScalesD_WeightNotNormal", "MixRubber17.PLC110-504.ScalesD_Status.Weight_NotNormal", "plc110.plc110_SCADA.Inp_40", 4),//!
            new Tag("ScalesD_Initial", "MixRubber17.PLC110-504.ScalesD_Status.Initial", "plc110.plc110_SCADA.Inp_40", 5),//!
            //new Tag("ScalesD_CommandOpen", "MixRubber17.PLC110-504.ScalesD_Status.CommandOpen", "plc110.plc110_SCADA.B7", 12),

            // new Tag("ScalesD_IsNotOpening","MixRubber17.PLC110-504.ScalesD_Fail.IsNotOpening", "plc110.plc110_SCADA.AV1", 8),
            // new Tag("ScalesD_IsNotClosing", "MixRubber17.PLC110-504.ScalesD_Fail.IsNotClosing", "plc110.plc110_SCADA.AV1", 9),
            // new Tag("ScalesD_BatcherFail", "MixRubber17.PLC110-504.ScalesD_Fail.BatcherFail", "plc110.plc110_SCADA.AV1", 10),
            //new Tag("ScalesD_BatcherNotWork", "MixRubber17.PLC110-504.ScalesD_Fail.Batcher_NotWork", "plc110.plc110_SCADA.AV4", 14),

            new Tag("ScalesD_CurNeed", "MixRubber17.PLC100_ScalesD.CurNeed", "VesiD.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesD_FullNeed", "MixRubber17.PLC100_ScalesD.FullNeed", "VesiD.FullNeed"), //Весы Д.Общий заданный вес
            // new Tag("ScalesD_Overweight", "MixRubber17.PLC100_ScalesD.Overweight", "VesiD.Pereves"), //Весы Д.Вес не норма
            new Tag("ScalesD_CurReal", "MixRubber17.PLC100_ScalesD.CurReal", "VesiD.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesD_FullReal", "MixRubber17.PLC100_ScalesD.FullReal", "VesiD.FullReal"), //Весы Д.Общий измеренный вес
            #endregion

            #region Scales E
            new Tag("BatcherE1", "MixRubber17.PLC110-504.BatchersE.BatcherE1", "plc110.plc110_SCADA.Inp_48", 13),
            new Tag("BatcherE2", "MixRubber17.PLC110-504.BatchersE.BatcherE2", "plc110.plc110_SCADA.Inp_48", 14),
            new Tag("BatcherE3", "MixRubber17.PLC110-504.BatchersE.BatcherE3", "plc110.plc110_SCADA.Inp_48", 15),

            new Tag("ScalesE_IsClosed", "MixRubber17.PLC110-504.ScalesE_Status.IsClosed", "plc110.plc110_SCADA.Inp_40", 8),
            new Tag("ScalesE_Weighing", "MixRubber17.PLC110-504.ScalesE_Status.Weighing", "plc110.plc110_SCADA.B19", 7),
            new Tag("ScalesE_IsBusy", "MixRubber17.PLC110-504.ScalesE_Status.Tare_IsBusy", "plc110.plc110_SCADA.Inp_40", 10),//!
            new Tag("ScalesE_WeightNotNormal", "MixRubber17.PLC110-504.ScalesE_Status.Weight_NotNormal", "plc110.plc110_SCADA.Inp_40", 12),//!
            new Tag("ScalesE_Initial", "MixRubber17.PLC110-504.ScalesE_Status.Initial", "plc110.plc110_SCADA.Inp_40", 13),//!
            //new Tag("ScalesE_CommandOpen", "MixRubber17.PLC110-504.ScalesE_Status.CommandOpen", "plc110.plc110_SCADA.B7", 13),

            new Tag("ScalesE_CurNeed", "MixRubber17.PLC100_ScalesE.CurNeed", "VesiE.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesE_FullNeed", "MixRubber17.PLC100_ScalesE.FullNeed", "VesiE.FullNeed"), //Весы E.Общий заданный вес
            // new Tag("ScalesE_Overweight", "MixRubber17.PLC100_ScalesE.Overweight", "VesiE.Pereves"), //Весы E.Вес не норма
            new Tag("ScalesE_CurReal", "MixRubber17.PLC100_ScalesE.CurReal", "VesiE.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesE_FullReal", "MixRubber17.PLC100_ScalesE.FullReal", "VesiE.FullReal"), //Весы E.Общий измеренный вес

            // new Tag("ScalesE_IsNotOpening", "MixRubber17.PLC110-504.ScalesE_Fail.IsNotOpening", "plc110.plc110_SCADA.AV1", 12),
            // new Tag("ScalesE_IsNotClosing", "MixRubber17.PLC110-504.ScalesE_Fail.IsNotClosing", "plc110.plc110_SCADA.AV1", 13),
            // new Tag("ScalesE_BatcherFail", "MixRubber17.PLC110-504.ScalesE_Fail.BatcherFail", "plc110.plc110_SCADA.AV1", 14),
            // new Tag("ScalesE_BatcherNotWork", "MixRubber17.PLC110-504.ScalesE_Fail.Batcher_NotWork", "plc110.plc110_SCADA.AV4", 15),
            #endregion

            #region Scales Sh
            new Tag("BatcherSh1", "MixRubber17.PLC110-507.BatchersSh.BatcherSh1", "plc110.plc110_RS17.Inp_64", 9),
            new Tag("BatcherSh2", "MixRubber17.PLC110-507.BatchersSh.BatcherSh2", "plc110.plc110_RS17.Inp_64", 10),
            new Tag("BatcherSh3", "MixRubber17.PLC110-507.BatchersSh.BatcherSh3", "plc110.plc110_RS17.Inp_64", 11),

            new Tag("ScalesSh_IsClosed", "MixRubber17.PLC110-507.ScalesSh_Status.IsClosed", "plc110.plc110_RS17.Inp_56", 0),
            new Tag("ScalesSh_Weighing", "MixRubber17.PLC110-504.ScalesSh_Status.Weighing", "plc110.plc110_SCADA.B19", 4),
            new Tag("ScalesSh_IsBusy", "MixRubber17.PLC110-507.ScalesSh_Status.Tare_IsBusy", "plc110.plc110_RS17.Inp_56", 2),//!
            new Tag("ScalesSh_WeightNotNormal", "MixRubber17.PLC110-507.ScalesSh_Status.Weight_NotNormal", "plc110.plc110_RS17.Inp_56", 4),//!
            new Tag("ScalesSh_Initial", "MixRubber17.PLC110-507.ScalesSh_Status.Initial", "plc110.plc110_RS17.Inp_56", 5),//!
            //new Tag("ScalesSh_CommandOpen", "MixRubber17.PLC110-507.ScalesSh_Status.CommandOpen", "plc110.plc110_RS17.B77", 12), //Команда Открыть весы Ш

            // new Tag("ScalesSh_IsNotOpening", "MixRubber17.PLC110-507.ScalesSh_Fail.IsNotOpening", "plc110.plc110_RS17.AV5", 0),
            // new Tag("ScalesSh_IsNotClosing", "MixRubber17.PLC110-507.ScalesSh_Fail.IsNotClosing", "plc110.plc110_RS17.AV5", 1),
            // new Tag("ScalesSh_BatcherFail", "MixRubber17.PLC110-504.ScalesSh_Fail.BatcherFail", "plc110.plc110_RS17.AV5", 2),
            // new Tag("ScalesSh_BatcherNotWork", "MixRubber17.PLC110-507.ScalesSh_Fail.Batcher_NotWork", "plc110.plc110_RS17.AV5", 12), //Навеска по весам Ш не включилась

            new Tag("ScalesSh_CurNeed", "MixRubber17.PLC100_ScalesSh.CurNeed", "VesiSh.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesSh_FullNeed", "MixRubber17.PLC100_ScalesSh.FullNeed", "VesiSh.FullNeed"), //Весы Ш.Общий заданный вес
            // new Tag("ScalesSh_Overweight", "MixRubber17.PLC100_ScalesSh.Overweight", "VesiSh.Pereves"), //Весы Ш.Вес не норма
            new Tag("ScalesSh_CurReal", "MixRubber17.PLC100_ScalesSh.CurReal", "VesiSh.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesSh_FullReal", "MixRubber17.PLC100_ScalesSh.FullReal", "VesiSh.FullReal"), //Весы Ш.Общий измеренный вес
            #endregion

            #region Scales Ju
            new Tag("BatcherJu1", "MixRubber17.PLC110-507.BatchersJu.BatcherJu1", "plc110.plc110_RS17.Inp_64", 1),
            new Tag("BatcherJu2", "MixRubber17.PLC110-507.BatchersJu.BatcherJu2", "plc110.plc110_RS17.Inp_64", 7),
            new Tag("BatcherJu3", "MixRubber17.PLC110-507.BatchersJu.BatcherJu3","plc110.plc110_RS17.Inp_64", 8),

            new Tag("ScalesJu_IsClosed", "MixRubber17.PLC110-507.ScalesJu_Status.IsClosed", "plc110.plc110_RS17.Inp_64", 0),
            new Tag("ScalesJu_Weighing", "MixRubber17.PLC110-504.ScalesJu_Status.Weighing", "plc110.plc110_SCADA.B8", 3),
            new Tag("ScalesJu_IsBusy", "MixRubber17.PLC110-507.ScalesJu_Status.Tare_IsBusy", "plc110.plc110_RS17.Inp_64", 3),  //Весы Ю.Предупреждения.Тара занята //!
            new Tag("ScalesJu_WeightNotNormal", "MixRubber17.PLC110-507.ScalesJu_Status.Weight_NotNormal", "plc110.plc110_RS17.Inp_64", 4),  // Весы Ю.Вес не норма.Перевес //!
            new Tag("ScalesJu_Initial", "MixRubber17.PLC110-507.ScalesJu_Status.Initial", "plc110.plc110_RS17.Inp_64", 5), //Весы Ю.Предупреждения.Исходное //!
            //new Tag("ScalesJu_CommandOpen", "MixRubber17.PLC110-507.ScalesJu_Status.CommandOpen", "plc110.plc110_RS17.B77", 14), //Команда Открыть весы Ю

            // new Tag("ScalesJu_IsNotOpening", "MixRubber17.PLC110-507.ScalesJu_Fail.IsNotOpening", "plc110.plc110_RS17.AV5", 6),
            // new Tag("ScalesJu_IsNotClosing", "MixRubber17.PLC110-507.ScalesJu_Fail.IsNotClosing", "plc110.plc110_RS17.AV5", 7),
            // new Tag("ScalesJu_BatcherFail", "MixRubber17.PLC110-504.ScalesJu_Fail.BatcherFail", "plc110.plc110_RS17.AV5", 8), вария весов Ю сигнал с дозаторов 
            // new Tag("ScalesJu_BatcherNotWork", "MixRubber17.PLC110-507.ScalesJu_Fail.Batcher_NotWork", "plc110.plc110_RS17.AV5", 14), //Навеска по весам Ю не включилась

            new Tag("ScalesJu_CurNeed", "MixRubber17.PLC100_ScalesJu.CurNeed", "VesiJu.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesJu_FullNeed", "MixRubber17.PLC100_ScalesJu.FullNeed", "VesiJu.FullNeed"), //Весы Ю.Общий заданный вес
            // new Tag("ScalesJu_Overweight", "MixRubber17.PLC100_ScalesJu.Overweight", "VesiJu.Pereves"), //Весы Ю.Вес не норма
            new Tag("ScalesJu_CurReal", "MixRubber17.PLC100_ScalesJu.CurReal", "VesiJu.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesJu_FullReal", "MixRubber17.PLC100_ScalesJu.FullReal", "VesiJu.FullReal"), //Весы Ю.Общий измеренный вес
            #endregion

            #region Scales Y
            new Tag("BatcherY1", "MixRubber17.PLC110-507.BatchersY.BatcherY1", "plc110.plc110_RS17.Inp_64", 12),
            new Tag("BatcherY2", "MixRubber17.PLC110-507.BatchersY.BatcherY2", "plc110.plc110_RS17.Inp_64", 13),
            new Tag("BatcherY3", "MixRubber17.PLC110-507.BatchersY.BatcherY3", "plc110.plc110_RS17.Inp_64", 14),
            new Tag("BatcherY4", "MixRubber17.PLC110-507.BatchersY.BatcherY4", "plc110.plc110_RS17.Inp_64", 15),

            new Tag("ScalesY_IsClosed", "MixRubber17.PLC110-507.ScalesY_Status.IsClosed", "plc110.plc110_RS17.Inp_56", 8),
            new Tag("ScalesY_Weighing", "MixRubber17.PLC110-504.ScalesY_Status.Weighing", "plc110.plc110_SCADA.B19", 3),
            new Tag("ScalesY_IsBusy", "MixRubber17.PLC110-507.ScalesY_Status.Tare_IsBusy", "plc110.plc110_RS17.Inp_56", 10),//!
            new Tag("ScalesY_WeightNotNormal", "MixRubber17.PLC110-507.ScalesY_Status.Weight_NotNormal", "plc110.plc110_RS17.Inp_56", 12),//!
            new Tag("ScalesY_Initial", "MixRubber17.PLC110-507.ScalesY_Status.Initial", "plc110.plc110_RS17.Inp_56", 13),//!
            //new Tag("ScalesY_CommandOpen", "MixRubber17.PLC110-507.ScalesY_Status.CommandOpen", "plc110.plc110_RS17.B77", 13), //Команда Открыть весы У

            // new Tag("ScalesY_IsNotOpening", "MixRubber17.PLC110-507.ScalesY_Fail.IsNotOpening", "plc110.plc110_RS17.AV5", 3),
            // new Tag("ScalesY_IsNotClosing", "MixRubber17.PLC110-507.ScalesY_Fail.IsNotClosing", "plc110.plc110_RS17.AV5", 4),
            // new Tag("ScalesY_BatcherFail", "MixRubber17.PLC110-504.ScalesY_Fail.BatcherFail", "plc110.plc110_RS17.AV5", 5), Авария весов У сигнал с дозаторов 
            // new Tag("ScalesY_BatcherNotWork", "MixRubber17.PLC110-507.ScalesY_Fail.Batcher_NotWork", "plc110.plc110_RS17.AV5", 13), //Навеска по весам У не включилась

            new Tag("ScalesY_CurNeed", "MixRubber17.PLC100_ScalesY.CurNeed", "VesiU.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesY_FullNeed", "MixRubber17.PLC100_ScalesY.FullNeed", "VesiU.FullNeed"), //Весы У.Общий заданный вес
            // new Tag("ScalesY_Overweight", "MixRubber17.PLC100_ScalesY.Overweight", "VesiU.Pereves"), //Весы У.Вес не норма
            new Tag("ScalesY_CurReal", "MixRubber17.PLC100_ScalesY.CurReal", "VesiU.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesY_FullReal", "MixRubber17.PLC100_ScalesY.FullReal", "VesiU.FullReal"), //Весы У.Общий измеренный вес
            #endregion

            #region Scales K
            new Tag("ScalesK_FullNeed", "MixRubber17.PLC100_ScalesK.FullNeed", "VesiK.CurNeed"), //Бункер.Заданный вес
            new Tag("ScalesK_FullReal", "MixRubber17.PLC100_ScalesK.FullReal", "VesiK.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesK_CurReal", "MixRubber17.PLC100_ScalesK.CurReal", "VesiK.CurReal"), //Бункер.Текущий вес

            new Tag("ScalesK_IsBusy", "MixRubber17.PLC110-504.ScalesK_Status.Tare_IsBusy", "plc110.plc110_SCADA.Inp_01", 12),//!
            new Tag("ScalesK_WeightNotNormal", "MixRubber17.PLC110-504.ScalesK_Status.Weight_NotNormal", "plc110.plc110_SCADA.Inp_01", 14),//!
            new Tag("ScalesK_Weighing", "MixRubber17.PLC110-504.ScalesK_Status.Weighing", "plc110.plc110_SCADA.B19", 10),

            // new Tag("ScalesK_Fail", "MixRubber17.PLC110-504.ScalesK_Fail.Fail", "plc110.plc110_SCADA.Inp_01", 15),
            #endregion

            #region MixRubber
            new Tag("DustCollectorOn", "MixRubber17.PLC110-504.MixRubber_Status.DustCollectorOn", "plc110.plc110_SCADA.Inp_02", 1),//Пылесборка вкл.
            new Tag("MR_PressureOff", "MixRubber17.PLC110-504.MixRubber_Status.PressureOff", "plc110.plc110_SCADA.Inp_02", 9),//!
            new Tag("MR_UpperPressUp", "MixRubber17.PLC110-504.MixRubber_Status.UpperPressUp", "plc110.plc110_SCADA.Inp_02", 10),//!
            new Tag("MR_UpperPressDown", "MixRubber17.PLC110-504.MixRubber_Status.UpperPressDown", "plc110.plc110_SCADA.Inp_02", 11),//!
            new Tag("MR_FlapValveClosed", "MixRubber17.MixRubber17.PLC110-504.MixRubber_Status.FlapValveClosed", "plc110.plc110_SCADA.Inp_02", 13),//1-закрыта //!
            new Tag("MR_PinClosed", "MixRubber17.PLC110-504.MixRubber_Status.PinClosed", "plc110.plc110_SCADA.Inp_02", 15),//!
            // new Tag("MR_RegimWork", "MixRubber17.PLC110-504.MixRubber_Status.RegimWork", "plc110.plc110_SCADA.XX6"), //Режим работы резиносмесителя. 0,1 - холостой;  2,3,4 - смесь; 3 - выгрузка

            // new Tag("Command_UpperPressUp", "MixRubber17.PLC110-504.MixRubber_Status.Command_UpperPressUp", "plc110.plc110_SCADA.Out_01", 3),//Команда Поднять Верхний пресс.Сигнал.Вход
            // new Tag("Command_PressureOff", "MixRubber17.PLC110-504.MixRubber_Status.Command_PressureOff", "plc110.plc110_SCADA.Out_01", 10,),//Команда Снять давление с ВП.Сигнал.Вход
            // new Tag("Command_UpperPressDown", "MixRubber17.PLC110-504.MixRubber_Status.Command_UpperPressDown", "plc110.plc110_SCADA.Out_01", 11),//Команда Опустить Верхний пресс.Сигнал.Вход
            // new Tag("Command_PinClose", "MixRubber17.PLC110-504.MixRubber_Status.Command_PinClose", "plc110.plc110_SCADA.Out_01", 12),//Команда Закрыть Клин
            // new Tag("Command_FlapValveClose", "MixRubber17.PLC110-504.MixRubber_Status.FlapValve_CommandClose", "plc110.plc110_SCADA.Out_01", 13),//Команда Закрыть Горбушу

            // new Tag("UpPress_PressureNotOff", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_PressureNotOff", "plc110.plc110_SCADA.AV3", 5),
            // new Tag("UpPress_NotUp", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_NotUp", "plc110.plc110_SCADA.AV3", 6),
            // new Tag("UpPress_NotDown", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_NotDown", "plc110.plc110_SCADA.AV3", 7),
            // new Tag("UpPress_UpAndDown", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_UpAndDown", "plc110.plc110_SCADA.AV3", 8),
            // new Tag("MR_Stoped", "MixRubber17.PLC110-504.MixRubber_Fail.MR_Stoped", "plc110.plc110_SCADA.AV3", 9), //
            // new Tag("Pin_NotClose", "MixRubber17.PLC110-504.MixRubber_Fail.Pin_NotClose", "plc110.plc110_SCADA.AV3", 12),
            // new Tag("Pin_NotOpen", "MixRubber17.PLC110-504.MixRubber_Fail.Pin_NotOpen", "plc110.plc110_SCADA.AV3", 13),
            // new Tag("FlapValve_NotOpened", "MixRubber17.PLC110-504.MixRubber_Fail.FlapValve_NotOpened", "plc110.plc110_SCADA.AV3", 14),
            // new Tag("FlapValve_NotClosed", "MixRubber17.PLC110-504.MixRubber_Fail.FlapValve_NotClosed", "plc110.plc110_SCADA.AV3", 15),
            new Tag("HighTemperature", "MixRubber17.PLC110-504.MixRubber_Fail.HighTemperature", "plc110.plc110_SCADA.AV4", 5),//Высокая температура в РС.Авария
            new Tag("CriticalTemperature", "MixRubber17.PLC110-504.MixRubber_Fail.CriticalTemperature", "plc110.plc110_SCADA.AV4", 6),//Критическая температура в РС.Авария
            // new Tag("RegMixing_Stoped", "MixRubber17.PLC110-504.MixRubber_Fail.RegMixing_Stoped", "plc110.plc110_SCADA.AV4", 7), // Останов РЖ ожидание подъема пресса.Авария
            // new Tag("SensorT1_Fail", "MixRubber17.PLC110-504.MixRubber_Fail.SensorT1_Fail", "plc110.plc110_SCADA.AV4", 8),//Авария верхней термопары Т1
            // new Tag("SensorT2_Fail", "MixRubber17.PLC110-504.MixRubber_Fail.SensorT2_Fail", "plc110.plc110_SCADA.AV4", 9),//Авария нижней термопары Т2
            #endregion

            #region Transporters
            // new Tag("K_CommandOn", "MixRubber17.PLC110-504.Transporters.K_CommandOn", "plc110.plc110_SCADA.B19", 13),
            new Tag("TK_Working", "MixRubber17.PLC110-504.Transporters.K_Working", "plc110.plc110_SCADA.Inp_02", 5),
            // new Tag("K_NotOn_RegLoad", "MixRubber17.PLC110-504.Transporters.K_NotOn_RegLoad", "plc110.plc110_SCADA.AV2", 4), // Тр-р К не включился от РГ
            // new Tag("K_NotStoped", "MixRubber17.PLC110-504.Transporters.K_NotStoped", "plc110.plc110_SCADA.AV2", 6),//Тр-р К не остановился

            new Tag("TL1_Working", "MixRubber17.PLC110-504.Transporters.L1_Working", "plc110.plc110_SCADA.B8", 10),
            // new Tag("L1_CommandOn", "MixRubber17.PLC110-504.Transporters.L1_CommandOn", "plc110.plc110_SCADA.B19", 12),
            // new Tag("L1_NotOn_RegMix1", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegMix1", "plc110.plc110_SCADA.AV2", 7), //Тр-р Л1 не включился от РЖ_открыт нижний затвор
            // new Tag("L1_NotOn_RegMix2", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegMix2", "plc110.plc110_SCADA.AV2", 8), //Тр-р Л1 не включился_нет верхнего положения поплавка
            // new Tag("L1_NotOff_RegMix", "MixRubber17.PLC110-504.Transporters.L1_NotOff_RegMix", "plc110.plc110_SCADA.AV2", 9), //	Тр-р Л1 не выключился от РЖ
            // new Tag("L1_NotOn_RegLoad", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegLoad", "plc110.plc110_SCADA.AV2", 10), // Тр-р Л1 не включился от РГ
            // new Tag("L1_NotStoped", "MixRubber17.PLC110-504.Transporters.L1_NotStoped","plc110.plc110_SCADA.AV2", 11), // Тр-р Л1 не остановился

            new Tag("TL2_Working", "MixRubber17.PLC110-504.Transporters.L2_Working","plc110.plc110_SCADA.B8", 11),
            // new Tag("L2_CommandOn", "MixRubber17.PLC110-504.Transporters.L2_CommandOn","plc110.plc110_SCADA.B19", 11),
            // new Tag("L2_NotOff", "MixRubber17.PLC110-504.Transporters.L2_NotOff","plc110.plc110_SCADA.AV3", 1),
            // new Tag("L2_NotStoped", "MixRubber17.PLC110-504.Transporters.L2_NotStoped", "plc110.plc110_SCADA.AV3", 2),
            #endregion

            #region OilPump
            new Tag("OilPump_Working", "MixRubber17.PLC110-504.OilPump.Working","plc110.plc110_SCADA.Inp_02", 7),
            // new Tag("OilPump_CommandOn", "MixRubber17.PLC110-504.Oil_Pump.CommandOn","plc110.plc110_SCADA.B19", 8),
            // new Tag("OilPump_NotOn", "MixRubber17.PLC110-504.OilPump.NotOn", "plc110.plc110_SCADA.AV2", 14),
            // new Tag("OilPump_NotOff", "MixRubber17.PLC110-504.OilPump.NotOff", "plc110.plc110_SCADA.AV2", 15),
            // new Tag("OilPump_NotOn_RegMix", "MixRubber17.PLC110-504.OilPump.NotOn_RegMix", "plc110.plc110_SCADA.AV3", 0),
            #endregion

            #region Information
            new Tag("Btn_StopProcess", "MixRubber17.PLC110-504.Information.StopProcess", "plc110.plc110_SCADA.Inp_01", 5), //КЗТ зажата
            new Tag("ManualMode", "MixRubber17.PLC110-504.Information.ManualMode", "plc110.plc110_SCADA.Inp_01", 7), //ручной режим
            new Tag("Batchers_Start1", "MixRubber17.PLC110-504.Information.Batchers_Start1", "plc110.plc110_SCADA.Out_01", 14), //  Пуск навесок1 = 1 или Пуск навесок2 = 1, тогда Пуск навесок = 1
            new Tag("Batchers_Start2", "MixRubber17.PLC110-504.Information.Batchers_Start2", "plc110.plc110_SCADA.Out_01", 15), // Пуск навесок1 = 0 и Пуск навесок2 = 0, тогда Пуск навесок = 0
            new Tag("BagFilter_Blow", "MixRubber17.PLC110-504.Information.BagFilter_Blow", "plc110.plc110_SCADA.Inp_40", 7), //Предупреждения.ПУРФ включен
                //!!!не используется!!! new Tag("ScrewPrepareOff", "MixRubber17.PLC110-504.Information.AddScrewOff", "plc110.plc110_SCADA.Inp_40", 9), //Предупреждения.Шнек подработкики не включен
            new Tag("Not24V", "MixRubber17.PLC110-504.Information.Not24V", "plc110.plc110_SCADA.Inp_40", 15), // Нет 24B
            new Tag("RecipeNumber", "MixRubber17.PLC110-504.Information.RecipeNum", "plc110.plc110_SCADA.R440_nomRZ"), //Рецепт, шифр смеси
                // new Tag("Clock_MR_Load", "MixRubber17.PLC110-504.Information.Clock_MR_Load", "plc110.plc110_SCADA.R634_RG"), // Время под давлением (!!! не нашли в скаде)
                // new Tag("Clock_MR_Mix", "MixRubber17.PLC110-504.Information.Clock_MR_Mix", "plc110.plc110_SCADA.R664_RJ"), // Время смешения (!!! не нашли в скаде)
            new Tag("MR_TimerRG", "MixRubber17.PLC110-504.Information.Timer_MR_Load", "plc110.plc110_SCADA.CV1_RG"),//Таймер РГ
            new Tag("MR_TimerRJ", "MixRubber17.PLC110-504.Information.Timer_MR_Mix", "plc110.plc110_SCADA.CV2_RJ"),//Таймер РЖ
            new Tag("Batchers_Ready", "MixRubber17.PLC110-504.Information.Batchers_Ready", "plc110.plc110_SCADA.B8", 2),//Навески готовы.Сигнал.
            new Tag("Filling_OnStart", "MixRubber17.PLC110-504.Information.Filling_OnStart", "plc110.plc110_SCADA.B8", 5),// Заправка на старте
            new Tag("LastFilling_ToPlan", "MixRubber17.PLC110-504.Information.LastFilling_ToPlan", "plc110.plc110_SCADA.B8", 8),//Последняя заправка по плану
            new Tag("TestMode_On", "MixRubber17.PLC110-504.Information.TestRegime_On", "plc110.plc110_SCADA.B19", 0), //тестовый режим включен
            new Tag("Resipe_IsNot", "MixRubber17.PLC110-504.Information.Recipe_IsNot", "plc110.plc110_SCADA.B19", 1), //Нет рецепта
            new Tag("MR_Mix_IsFobidden", "MixRubber17.PLC110-504.Information.MR_Mix_IsForbidden", "plc110.plc110_SCADA.B19", 14), //Запрет РЖ
            new Tag("MR_Load_IsFobidden", "MixRubber17.PLC110-504.Information.MR_Load_IsForbidden", "plc110.plc110_SCADA.B19", 15),//Запрет РГ
            new Tag("EmergencyButton_Pushed", "MixRubber17.PLC110-504.Information.EmergencyButton_Pushed", "plc110.plc110_SCADA.AV4", 0),//Предупреждения.Нажат аварийник
            new Tag("StopButton_Pushed", "MixRubber17.PLC110-504.Information.StopButton_Pushed", "plc110.plc110_SCADA.AV4", 1),//Кнопка _СТОП_ нажата.Авария
            // new Tag("UnloadTempr_NotSet", "MixRubber17.PLC110-504.Information.UnloadTempr_NotSet", "plc110.plc110_SCADA.AV4", 0),// Не задана температура выгрузки
            #endregion

            #region Test Mode
            // new Tag("TestCommand_UpPressDown", "MixRubber17.PLC110-504.TestRegim.Command_UpPressDown", "plc110.plc110_SCADA.B270"), //опустить верхний пресс
            // new Tag("TestCommand_FlapValveClose", "MixRubber17.PLC110-504.TestRegim.Command_FlapValveClose", "plc110.plc110_SCADA.B271"), // закрыть НЗ
            // new Tag("Command_ScalesJOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesJOpen", "plc110.plc110_RS17.B26", 5), // Команда.Открыть весы Ж
            // new Tag("Command_ScalesDOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesDOpen", "plc110.plc110_RS17.B26", 6), // Команда.Открыть весы D	
            // new Tag("Command_ScalesEOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesEOpen", "plc110.plc110_RS17.B26", 7), // Команда.Открыть весы E
            // new Tag("Command_OilPumpOn", "MixRubber17.PLC110-507.TestRegim.Command_OilPumpOn", "plc110.plc110_RS17.B26", 9), // Команда.Включить маслонасос
            // new Tag("Command_TransporterKOn", "MixRubber17.PLC110-507.TestRegim.Command_TransporterKOn", "plc110.plc110_RS17.B26", 10), // Команда.Включить транспортер K
            // new Tag("Command_TransporterL2On", "MixRubber17.PLC110-507.TestRegim.Command_TransporterL2On", "plc110.plc110_RS17.B26", 11), // Команда.Включить транспортер Л2
            // new Tag("Command_TransporterL1On", "MixRubber17.PLC110-507.TestRegim.Command_TransporterL1On", "plc110.plc110_RS17.B26", 12), // Команда.Включить транспортер Л1
            // new Tag("TemperatureIsDanger", "MixRubber17.PLC110-507.TestRegim.TemperatureIsDanger", "plc110.plc110_RS17.B26", 13)// Температура критическая
            // new Tag("TBatchersOn", "MixRubber17.PLC110-507.TestRegim.BatchersOn", "plc110.plc110_RS17.B26", 14)// Навески включены
            // new Tag("Command_PressureOff", "MixRubber17.PLC110-507.TestRegim.Command_PressureOff", "plc110.plc110_RS17.B26", 15)// Команда давление снять
            // new Tag("Command_UpPress_Down_Up", "MixRubber17.PLC110-507.TestRegim.Command_UpPress_Down_Up", "plc110.plc110_RS17.B27", 0)// Поднять/опустить верхний пресс (B27-0)
            // new Tag("Command_ScalesShOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesShOpen", "plc110.plc110_RS17.B27", 4)// Открыть весы Ш
            // new Tag("Command_ScalesSСOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesСOpen", "plc110.plc110_RS17.B27", 5)// Открыть весы С
            // new Tag("Command_ScrewTC_On", "MixRubber17.PLC110-507.TestRegim.Command_ScrewTC_On", "plc110.plc110_RS17.B27", 6)// Включить шнек ТУ		
            // new Tag("Command_ContainerC_Open", "MixRubber17.PLC110-507.TestRegim.Command_ContainerC_Open", "plc110.plc110_RS17.B27", 7)// Открыть сборную	ТУ		
            // new Tag("Command_ScalesYOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesYOpen", "plc110.plc110_RS17.B27", 13)// Открыть весы У			
            // new Tag("Command_ScalesJuOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesJuOpen", "plc110.plc110_RS17.B27", 14)// Открыть весы Ю	
            #endregion

            #region Temperature water
            /* Пока нет необходимости
            new Tag("WaterT1", "MixRubber17.PLC110-504.TableTWater.T1", "plc110.plc110_analog.Tvodi1"), // Температура воды.Т1- подача	
            new Tag("WaterT2", "MixRubber17.PLC110-504.TableTWater.T2", "plc110.plc110_analog.Tvodi2"), // Температура воды.Т2
            new Tag("WaterT3", "MixRubber17.PLC110-504.TableTWater.T3", "plc110.plc110_analog.Tvodi3"), //Температура воды.Т3	
            new Tag("WaterT4", "MixRubber17.PLC110-504.TableTWater.T4", "plc110.plc110_analog.Tvodi4"), //Температура воды.Т4					
            new Tag("WaterT5", "MixRubber17.PLC110-504.TableTWater.T5", "plc110.plc110_analog.Tvodi5"), //Температура воды.Т5						
            new Tag("WaterRotor1", "MixRubber17.PLC110-504.TableTWater.Rotor1", "plc110.plc110_analog.Tv_rot1_6_"), //Температура воды.ротор1			
            new Tag("WaterRotor2", "MixRubber17.PLC110-504.TableTWater.Rotor2", "plc110.plc110_analog.Tv_rot2_7_"), //Температура воды.ротор2
            */
            #endregion

            #region String value
            new Tag("OperatorFIO", "MixRubber17.PLC110-504.StringValues.FIO", "plc110.plc110_string.FIO"), //ФИО оператора					
            new Tag("RecipeName", "MixRubber17.PLC110-504.StringValues.RecipeName", "plc110.plc110_string.Name_rez"), //Имя рецепта					
            new Tag("ModeName", "MixRubber17.PLC110-504.StringValues.RegimeName", "plc110.plc110_string.Name_rej"), //Режим.Имя		
            new Tag("ShiftName", "MixRubber17.PLC110-504.StringValues.ShiftName", "plc110.plc110_string.NameSmena"), //Смена.Имя
            new Tag("NumberLoad", "MixRubber17.PLC110-504.StringValues.NomN_110", "plc110.plc110_RS17.NomN_110"), //Номер навески из ПЛК 110
            #endregion

            #region Группы тегов в Lectus: Монитор,ARMO порт 505
            // new Tag("ShiftNumber", "MixRubber17.PPLC110-505.ShiftNum", "plc110_Monitor.R436_bykva"), //Номер выбранной смены A-1, Б-2, В-3, Г-4	//нет необходимости выводить на экран
            new Tag("ModeNumber", "MixRubber17.PLC110-505.NumReg", "plc110_Monitor.R438_nomRJ"),//Номер загруженного режима в ПЛК
            new Tag("FullTimeMix", "MixRubber17.PLC110-505.FullTimeMix", "plc110_Monitor.CV5_RJ"),//Время смешения полное от пуска РЖ
            new Tag("TimeMixUnderPress", "MixRubber17.PLC110-505.TimeMixUnderPress", "plc110_Monitor.CV6_d"),//Время смешения под давлением
            new Tag("NumLoading_Shift", "MixRubber17.PLC110-505.NumLoading_Shift", "plc110_Monitor.CV8_sm"),//Номер заправки от начала смены
            new Tag("NumLoading_Recept", "MixRubber17.PLC110-505.NumLoading_Recept", "plc110_Monitor.CV7_rej"),//Номер заправки текущего рецепта
            // new Tag("PressUp_Position", "MixRubber17.PLC110-505.PressUp_Position", "plc110_Monitor.Рмм"),//Положение верхнего пресса в мм
            new Tag("Plan_Loadings", "MixRubber17.PLC110-505.Plan_Loadings", "plc110_ARMO.R439_plan"),//План заправок
            new Tag("MR_Mix_IsGoing", "MixRubber17.PLC110-505.RegMix_IsGoing", "plc110_ARMO.Zapret"),//Идет цикл смешения, запрет загрузки	
            // new Tag("RegMix_On", "MixRubber17.PLC110-505.RegMix_On", "plc110_ARMO.Zagl_100"),//Пуск режима смешения (1 -включен)

            new Tag("TUnloading", "MixRubber17.PLC110-505.TUnloading", "plc110_ARMO.R441_Tzad"),//Температура выгрузки заданная
            new Tag("TCorrect", "MixRubber17.PLC110-505.TCorret", "plc110_ARMO.R442_Pop"),//Поправка температуры заданная
            new Tag("TDanger", "MixRubber17.PLC110-505.TDanger", "plc110_ARMO.R443_Tkri"),//Температура критическая  заданная
            new Tag("TUnloadSensor", "MixRubber17.PLC110-505.TFact", "plc110_Monitor.R649_Tfakt"),//Температура  выгрузки по датчику

            new Tag("TUnloading_WithCorrect", "MixRubber17.PLC110-505.TUnloading_WithCorrect", "plc110_Monitor.R655_Tv_P"),//Температура выгрузки заданная минус поправка
            new Tag("TDanger_WithCorrect", "MixRubber17.PLC110-505.TDanger_WithCorrect", "plc110_Monitor.R656_Tk_P"),//Температура критическая  минус поправка

            new Tag("MR_Power", "MixRubber17.PLC110-505.Power", "plc110_Monitor.Power"),//Ток с датчика (мощщность)	 
            new Tag("MR_TSensorUp", "MixRubber17.PLC110-505.TSensor1_Up", "plc110_Monitor.Temp1"),//Температура смеси текущая с датчика 1 (верхняя термопара) 
            // new Tag("MR_TSensorDown", "MixRubber17.PLC110-505.TSensor2_Down", "plc110_Monitor.Temp2"),//Температура смеси текущая с датчика 2 (нижняя термопара)  //нет необходимости
            #endregion
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
                _OPCServer.Connect(IsAlternativeOPC ? AlernativeServerName : ServerName, "");
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
                {
                    if (item.AlternativePath != "")
                        _OPCGroup.OPCItems.AddItem(item.AlternativePath, item.ClientHandle);
                }
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
            {
                _OPCServer.Disconnect();
            }
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
