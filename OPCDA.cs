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
            #region Scales C
            new Tag("BatcherC1", "MixRubber17.PLC110-504.BatchersC.BatcherC1"),
            new Tag("BatcherC2", "MixRubber17.PLC110-504.BatchersC.BatcherC2"),
            new Tag("BatcherC3", "MixRubber17.PLC110-504.BatchersC.BatcherC3"),
            new Tag("BatcherC4", "MixRubber17.PLC110-504.BatchersC.BatcherC4"),
            new Tag("BatcherC5", "MixRubber17.PLC110-504.BatchersC.BatcherC5"),
        
            new Tag("ScalesC_IsClosed", "MixRubber17.PLC110-504.ScalesC_Status.IsClosed"),
            new Tag("ScalesC_IsOpened", "MixRubber17.PLC110-504.ScalesC_Status.IsOpened"),
            new Tag("ScalesC_Weighing", "MixRubber17.PLC110-504.ScalesC_Status.Weighing"),
            // new Tag("ScalesC_Tare_IsBusy", "MixRubber17.PLC110-504.ScalesC_Status.Tare_IsBusy"),
            // new Tag("ScalesC_Weight_NotNormal", "MixRubber17.PLC110-504.ScalesC_Status.Weight_NotNormal"),
            // new Tag("ScalesC_Initial", "MixRubber17.PLC110-504.ScalesC_Status.Initial"),
            // new Tag("ScalesC_CommandOpen", "MixRubber17.PLC110-504.ScalesC_Status.CommandOpen"),

            // new Tag("ScalesC_IsNotOpening", "MixRubber17.PLC110-504.ScalesC_Fail.IsNotOpening"),
            // new Tag("ScalesC_IsNotClosing", "MixRubber17.PLC110-504.ScalesC_Fail.IsNotClosing"),
            // new Tag("ScalesC_BatcherFail", "MixRubber17.PLC110-504.ScalesC_Fail.BatcherFail"),
            // new Tag("ScalesC_BatcherNotWork", "MixRubber17.PLC110-504.ScalesC_Fail.Batcher_NotWork"),
            // new Tag("ScalesC_ClosedAndOpened", "MixRubber17.PLC110-504.ScalesC_Fail.ClosedAndOpened"),

            new Tag("ScalesC_CurNeed","MixRubber17.PLC100_ScalesC.CurNeed","plc110.VesiC.CurNeed"),
            new Tag("ScalesC_FullNeed","MixRubber17.PLC100_ScalesC.FullNeed","plc110.VesiC.FullNeed"),
            // new Tag("ScalesC_Overweight","MixRubber17.PLC100_ScalesC.Overweight","plc110.VesiC.Pereves"), //Весы С.Вес не норма
            new Tag("ScalesC_CurReal","MixRubber17.PLC100_ScalesC.CurReal","plc110.VesiC.FullReal"), // Бункер.Измеренный вес
            new Tag("ScalesC_FullReal","MixRubber17.PLC100_ScalesC.FullReal","plc110.VesiC.FullReal"), //Весы С.Данные.Общий измеренный вес
            // new Tag("ScalesC_KRP","MixRubber17.PLC100_ScalesC.KRP","plc110.VesiC.KRP"), //Весы С.Обработка КРП
            #endregion

            #region Conteiner C
            // new Tag("ContainerC_IsClosed", "MixRubber17.PLC110-504.ContainerC_Status.IsClosed"),
            // new Tag("ContainerC_IsOpened", "MixRubber17.PLC110-504.ContainerC_Status.IsOpened"),
            // new Tag("ContainerC_BatcherNum", "MixRubber17.PLC110-504.ContainerC_Status.BatcherNum", "plc110.plc110_SCADA.CV0_na_C"),
            // new Tag("ContainerC_CommandOpen", "MixRubber17.PLC110-504.ContainerC_Status.CommandOpen"),
            // new Tag("ContainerC_Weighting", "MixRubber17.PLC110-504.ContainerC_Status.Weighting"),

            // new Tag("ContainerC_IsNotClosing", "MixRubber17.PLC110-504.ContainerC_Fail.IsNotClosing"),
            // new Tag("ContainerC_IsNotOpening", "MixRubber17.PLC110-504.ContainerC_Fail.IsNotOpening"),
            // new Tag("ContainerC_ClosedAndOpened", "MixRubber17.PLC110-504.ContainerC_Fail.ClosedAndOpened"),
            // new Tag("ContainerC_NotFull", "MixRubber17.PLC110-504.ContainerC_Fail.NotFull"),

            // new Tag("ScrewTC_Working", "MixRubber17.PLC110-504.ScrewTC.Working"),
            // new Tag("ScrewTC_CommandOn", "MixRubber17.PLC110-504.ScrewTC.CommandOn"),
            // new Tag("ScrewTC_NotOn", "MixRubber17.PLC110-504.ScrewTC.NotOn"),
            // new Tag("ScrewTC_NotStoped", "MixRubber17.PLC110-504.ScrewTC.NotStoped"),
            #endregion

            #region Scales J
            new Tag("BatcherJ1", "MixRubber17.PLC110-504.BatchersJ.BatcherJ1"),
            new Tag("BatcherJ2", "MixRubber17.PLC110-504.BatchersJ.BatcherJ2"),
            new Tag("BatcherJ3", "MixRubber17.PLC110-504.BatchersJ.BatcherJ3"),
            new Tag("BatcherJ4", "MixRubber17.PLC110-504.BatchersJ.BatcherJ4"),

            new Tag("ScalesJ_IsOpened", "MixRubber17.PLC110-504.ScalesJ_Status.IsOpened"),
            new Tag("ScalesJ_Weighing", "MixRubber17.PLC110-504.ScalesJ_Status.Weighing"),
            //new Tag("ScalesJ_Tare_IsBusy", "MixRubber17.PLC110-504.ScalesJ_Status.Tare_IsBusy"),
            //new Tag("ScalesJ_TWeight_NotNormal", "MixRubber17.PLC110-504.ScalesJ_Status.Weight_NotNormal"),
            //new Tag("ScalesJ_Initial", "MixRubber17.PLC110-504.ScalesJ_Status.Initial"),
            //new Tag("ScalesJ_CommandOpen", "MixRubber17.PLC110-504.ScalesJ_Status.CommandOpen"),

            //new Tag("ScalesJ_BatcherNotWork", "MixRubber17.PLC110-504.ScalesJ_Fail.Batcher_NotWork"),
            // new Tag("ScalesJ_IsNotOpening", "MixRubber17.PLC110-504.ScalesJ_Fail.IsNotOpening"),
            // new Tag("ScalesJ_IsNotClosing", "MixRubber17.PLC110-504.ScalesJ_Fail.IsNotClosing"),
            // new Tag("ScalesJ_BatcherFail", "MixRubber17.PLC110-504.ScalesJ_Fail.BatcherFail"),

            new Tag("ScalesJ_CurNeed","MixRubber17.PLC100_ScalesJ.CurNeed","plc110.VesiJ.CurNeed"), // Бункер.Заданный вес
            new Tag("ScalesJ_FullNeed","MixRubber17.PLC100_ScalesJ.FullNeed","plc110.VesiJ.FullNeed"), //Весы Ж.Общий заданный вес
            // new Tag("ScalesJ_Overweight","MixRubber17.PLC100_ScalesJ.Overweight","plc110.VesiJ.Pereves"), //Весы Ж.Вес не норма
            new Tag("ScalesJ_CurReal","MixRubber17.PLC100_ScalesJ.CurReal","plc110.VesiJ.FullReal"), //Бункер.Измеренный вес
            new Tag("ScalesJ_FullReal","MixRubber17.PLC100_ScalesJ.FullReal","plc110.VesiJ.FullReal"), //Весы Ж.Общий измеренный вес
            // new Tag("ScalesJ_KRP","MixRubber17.PLC100_ScalesJ.KRP","plc110.VesiJ.KRP"), //Весы Ж.Обработка КРП
            #endregion

            //new Tag("Injector_Weighting", "MixRubber17.PLC110-504.Injector.Weighting"),
            //new Tag("Injector_NotEmpty", "MixRubber17.PLC110-504.Injector.NotEmpty"),
            //new Tag("Injector_NotFull", "MixRubber17.PLC110-504.Injector.NotFull"),

            new Tag("BatcherD1", "MixRubber17.PLC110-504.BatchersD.BatcherD1"),
new Tag("BatcherD2", "MixRubber17.PLC110-504.BatchersD.BatcherD2"),
new Tag("BatcherD3", "MixRubber17.PLC110-504.BatchersD.BatcherD3"),
new Tag("BatcherD4", "MixRubber17.PLC110-504.BatchersD.BatcherD4"),

new Tag("ScalesD_IsOpened", "MixRubber17.PLC110-504.ScalesD_Status.IsOpened"),
new Tag("ScalesD_Weighing", "MixRubber17.PLC110-504.ScalesD_Status.Weighing"),
//new Tag("ScalesD_Tare_IsBusy", "MixRubber17.PLC110-504.ScalesD_Status.Tare_IsBusy"),
//new Tag("ScalesD_Weight_NotNormal", "MixRubber17.PLC110-504.ScalesD_Status.Weight_NotNormal"),
//new Tag("ScalesD_Initial", "MixRubber17.PLC110-504.ScalesD_Status.Initial"),
//new Tag("ScalesD_CommandOpen", "MixRubber17.PLC110-504.ScalesD_Status.CommandOpen"),

// new Tag("ScalesD_IsNotOpening", "MixRubber17.PLC110-504.ScalesD_Fail.IsNotOpening"),
// new Tag("ScalesD_IsNotClosing", "MixRubber17.PLC110-504.ScalesD_Fail.IsNotClosing"),
// new Tag("ScalesD_BatcherFail", "MixRubber17.PLC110-504.ScalesD_Fail.BatcherFail"),
//new Tag("ScalesD_BatcherNotWork", "MixRubber17.PLC110-504.ScalesD_Fail.Batcher_NotWork"),

new Tag("ScalesD_CurNeed","MixRubber17.PLC100_ScalesD.CurNeed","plc110.VesiD.CurNeed"), //Бункер.Заданный вес
new Tag("ScalesD_FullNeed","MixRubber17.PLC100_ScalesD.FullNeed","plc110.VesiD.FullNeed"), //Весы Д.Общий заданный вес
// new Tag("ScalesD_Overweight","MixRubber17.PLC100_ScalesD.Overweight","plc110.VesiD.Pereves"), //Весы Д.Вес не норма
new Tag("ScalesD_CurReal","MixRubber17.PLC100_ScalesD.CurReal","plc110.VesiD.FullReal"), //Бункер.Измеренный вес
new Tag("ScalesD_FullReal","MixRubber17.PLC100_ScalesD.FullReal","plc110.VesiD.FullReal"), //Весы Д.Общий измеренный вес
// new Tag("ScalesD_KRP","MixRubber17.PLC100_ScalesD.KRP","plc110.VesiD.KRP"),//Весы Д.Обработка КРП

new Tag("BatcherE1", "MixRubber17.PLC110-504.BatchersE.BatcherE1"),
new Tag("BatcherE2", "MixRubber17.PLC110-504.BatchersE.BatcherE2"),
new Tag("BatcherE3", "MixRubber17.PLC110-504.BatchersE.BatcherE3"),

new Tag("ScalesE_IsOpened", "MixRubber17.PLC110-504.ScalesE_Status.IsOpened"),
new Tag("ScalesE_Weighing", "MixRubber17.PLC110-504.ScalesE_Status.Weighing"),
//new Tag("ScalesE_Tare_IsBusy", "MixRubber17.PLC110-504.ScalesE_Status.Tare_IsBusy"),
//new Tag("ScalesE_Weight_NotNormal", "MixRubber17.PLC110-504.ScalesE_Status.Weight_NotNormal"),
//new Tag("ScalesE_Initial", "MixRubber17.PLC110-504.ScalesE_Status.Initial"),
//new Tag("ScalesE_CommandOpen", "MixRubber17.PLC110-504.ScalesE_Status.CommandOpen"),

new Tag("ScalesE_CurNeed","MixRubber17.PLC100_ScalesE.CurNeed","plc110.VesiE.CurNeed"), //Бункер.Заданный вес
new Tag("ScalesE_FullNeed","MixRubber17.PLC100_ScalesE.FullNeed","plc110.VesiE.FullNeed"), //Весы E.Общий заданный вес
// new Tag("ScalesE_Overweight","MixRubber17.PLC100_ScalesE.Overweight","plc110.VesiE.Pereves"), //Весы E.Вес не норма
new Tag("ScalesE_CurReal","MixRubber17.PLC100_ScalesE.CurReal","plc110.VesiE.FullReal"), //Бункер.Измеренный вес
new Tag("ScalesE_FullReal","MixRubber17.PLC100_ScalesE.FullReal","plc110.VesiE.FullReal"), //Весы E.Общий измеренный вес
// new Tag("ScalesE_KRP","MixRubber17.PLC100_ScalesE.KRP","plc110.VesiE.KRP"),//Весы E.Обработка КРП


// new Tag("ScalesE_IsNotOpening", "MixRubber17.PLC110-504.ScalesE_Fail.IsNotOpening"),
// new Tag("ScalesE_IsNotClosing", "MixRubber17.PLC110-504.ScalesE_Fail.IsNotClosing"),
// new Tag("ScalesE_BatcherFail", "MixRubber17.PLC110-504.ScalesE_Fail.BatcherFail"),
// new Tag("ScalesE_ClosedAndOpened", "MixRubber17.PLC110-504.ScalesE_Fail.ClosedAndOpened"),
//new Tag("ScalesE_BatcherNotWork", "MixRubber17.PLC110-504.ScalesE_Fail.Batcher_NotWork"),

new Tag("BatcherSh1", "MixRubber17.PLC110-507.BatchersSh.BatcherSh1"),
new Tag("BatcherSh2", "MixRubber17.PLC110-507.BatchersSh.BatcherSh2"),
new Tag("BatcherSh3", "MixRubber17.PLC110-507.BatchersSh.BatcherSh3"),

new Tag("ScalesSh_IsOpened", "MixRubber17.PLC110-507.ScalesSh_Status.IsOpened"),
new Tag("ScalesSh_Weighing", "MixRubber17.PLC110-504.ScalesSh_Status.Weighing"),
//new Tag("ScalesSh_Tare_IsBusy", "MixRubber17.PLC110-507.ScalesSh_Status.Tare_IsBusy"),
//new Tag("ScalesSh_Weight_NotNormal", "MixRubber17.PLC110-507.ScalesSh_Status.Weight_NotNormal"),
//new Tag("ScalesSh_Initial", "MixRubber17.PLC110-507.ScalesSh_Status.Initial"),
//new Tag("ScalesSh_CommandOpen", "MixRubber17.PLC110-507.ScalesSh_Status.CommandOpen"), //Команда Открыть весы Ш

// new Tag("ScalesSh_IsNotOpening", "MixRubber17.PLC110-507.ScalesSh_Fail.IsNotOpening"),
// new Tag("ScalesSh_IsNotClosing", "MixRubber17.PLC110-507.ScalesSh_Fail.IsNotClosing"),
// new Tag("ScalesSh_BatcherFail", "MixRubber17.PLC110-504.ScalesSh_Fail.BatcherFail"),
// new Tag("ScalesSh_BatcherNotWork", "MixRubber17.PLC110-507.ScalesSh_Fail.Batcher_NotWork"), //Навеска по весам Ш не включилась

new Tag("ScalesSh_CurNeed","MixRubber17.PLC100_ScalesSh.CurNeed","plc110.VesiSh.CurNeed"), //Бункер.Заданный вес
new Tag("ScalesSh_FullNeed","MixRubber17.PLC100_ScalesSh.FullNeed","plc110.VesiSh.FullNeed"), //Весы Ш.Общий заданный вес
// new Tag("ScalesSh_Overweight","MixRubber17.PLC100_ScalesSh.Overweight","plc110.VesiSh.Pereves"), //Весы Ш.Вес не норма
new Tag("ScalesSh_CurReal","MixRubber17.PLC100_ScalesSh.CurReal","plc110.VesiSh.FullReal"), //Бункер.Измеренный вес
new Tag("ScalesSh_FullReal","MixRubber17.PLC100_ScalesSh.FullReal","plc110.VesiSh.FullReal"), //Весы Ш.Общий измеренный вес
// new Tag("ScalesSh_KRP","MixRubber17.PLC100_ScalesSh.KRP","plc110.VesiSh.KRP"),//Весы Ш.Обработка КРП


new Tag("BatcherJu1", "MixRubber17.PLC110-507.BatchersJu.BatcherJu1"),
new Tag("BatcherJu2", "MixRubber17.PLC110-507.BatchersJu.BatcherJu2"),
new Tag("BatcherJu3", "MixRubber17.PLC110-507.BatchersJu.BatcherJu3"),

new Tag("ScalesJu_IsOpened", "MixRubber17.PLC110-507.ScalesJu_Status.IsOpened"),
new Tag("ScalesJu_Weighing", "MixRubber17.PLC110-504.ScalesJu_Status.Weighing"),
//new Tag("ScalesJU_Tare_IsBusy", "MixRubber17.PLC110-507.ScalesJU_Status.Tare_IsBusy"),  //Весы Ю.Предупреждения.Тара занята
//new Tag("ScalesJU_Weight_NotNormal", "MixRubber17.PLC110-507.ScalesJU_Status.Weight_NotNormal"),  // Весы Ю.Вес не норма.Перевес
//new Tag("ScalesJU_Initial", "MixRubber17.PLC110-507.ScalesJU_Status.Initial"),  //Весы Ю.Предупреждения.Исходное
//new Tag("ScalesJU_CommandOpen", "MixRubber17.PLC110-507.ScalesJU_Status.CommandOpen"), //Команда Открыть весы Ю

// new Tag("ScalesJU_IsNotOpening", "MixRubber17.PLC110-507.ScalesJU_Fail.IsNotOpening"),
// new Tag("ScalesJU_IsNotClosing", "MixRubber17.PLC110-507.ScalesJU_Fail.IsNotClosing"),
// new Tag("ScalesJU_BatcherFail", "MixRubber17.PLC110-504.ScalesJU_Fail.BatcherFail"), вария весов Ю сигнал с дозаторов 
// new Tag("ScalesJU_BatcherNotWork", "MixRubber17.PLC110-507.ScalesJU_Fail.Batcher_NotWork"), //Навеска по весам Ю не включилась

new Tag("ScalesJU_CurNeed","MixRubber17.PLC100_ScalesJU.CurNeed","plc110.VesiJU.CurNeed"), //Бункер.Заданный вес
new Tag("ScalesJU_FullNeed","MixRubber17.PLC100_ScalesJU.FullNeed","plc110.VesiJU.FullNeed"), //Весы Ю.Общий заданный вес
// new Tag("ScalesJU_Overweight","MixRubber17.PLC100_ScalesJU.Overweight","plc110.VesiJU.Pereves"), //Весы Ю.Вес не норма
new Tag("ScalesJU_CurReal","MixRubber17.PLC100_ScalesJU.CurReal","plc110.VesiJU.FullReal"), //Бункер.Измеренный вес
new Tag("ScalesJU_FullReal","MixRubber17.PLC100_ScalesJU.FullReal","plc110.VesiJU.FullReal"), //Весы Ю.Общий измеренный вес
// new Tag("ScalesJU_KRP","MixRubber17.PLC100_ScalesJU.KRP","plc110.VesiJU.KRP"),//Весы Ю.Обработка КРП

new Tag("BatcherY1", "MixRubber17.PLC110-507.BatchersY.BatcherY1"),
new Tag("BatcherY2", "MixRubber17.PLC110-507.BatchersY.BatcherY2"),
new Tag("BatcherY3", "MixRubber17.PLC110-507.BatchersY.BatcherY3"),

new Tag("ScalesY_IsOpened", "MixRubber17.PLC110-507.ScalesY_Status.IsOpened"),
new Tag("ScalesY_Weighing", "MixRubber17.PLC110-504.ScalesY_Status.Weighing"),
//new Tag("ScalesY_Tare_IsBusy", "MixRubber17.PLC110-507.ScalesY_Status.Tare_IsBusy"),
//new Tag("ScalesY_Weight_NotNormal", "MixRubber17.PLC110-507.ScalesY_Status.Weight_NotNormal"),
//new Tag("ScalesY_Initial", "MixRubber17.PLC110-507.ScalesY_Status.Initial"),
//new Tag("ScalesY_CommandOpen", "MixRubber17.PLC110-507.ScalesY_Status.CommandOpen"), //Команда Открыть весы У

// new Tag("ScalesY_IsNotOpening", "MixRubber17.PLC110-507.ScalesY_Fail.IsNotOpening"),
// new Tag("ScalesY_IsNotClosing", "MixRubber17.PLC110-507.ScalesY_Fail.IsNotClosing"),
// new Tag("ScalesY_BatcherFail", "MixRubber17.PLC110-504.ScalesY_Fail.BatcherFail"), Авария весов У сигнал с дозаторов 
// new Tag("ScalesY_BatcherNotWork", "MixRubber17.PLC110-507.ScalesY_Fail.Batcher_NotWork"), //Навеска по весам У не включилась

new Tag("ScalesY_CurNeed","MixRubber17.PLC100_ScalesY.CurNeed","plc110.VesiY.CurNeed"), //Бункер.Заданный вес
new Tag("ScalesY_FullNeed","MixRubber17.PLC100_ScalesY.FullNeed","plc110.VesiY.FullNeed"), //Весы У.Общий заданный вес
// new Tag("ScalesY_Overweight","MixRubber17.PLC100_ScalesY.Overweight","plc110.VesiY.Pereves"), //Весы У.Вес не норма
new Tag("ScalesY_CurReal","MixRubber17.PLC100_ScalesY.CurReal","plc110.VesiY.FullReal"), //Бункер.Измеренный вес
new Tag("ScalesY_FullReal","MixRubber17.PLC100_ScalesY.FullReal","plc110.VesiY.FullReal"), //Весы У.Общий измеренный вес
// new Tag("ScalesY_KRP","MixRubber17.PLC100_ScalesY.KRP","plc110.VesiY.KRP"),//Весы У.Обработка КРП


////////////////////////////////////////////////////////////////////////////////
///
//new Tag("ScalesK_CurNeed","MixRubber17.PLC100_ScalesK.CurNeed","plc110.VesiK.CurNeed"), //Бункер.Заданный вес
//new Tag("ScalesK_FullNeed","MixRubber17.PLC100_ScalesK.FullNeed","plc110.VesiK.FullNeed"), //Весы K.Общий заданный вес
// new Tag("ScalesK_Overweight","MixRubber17.PLC100_ScalesK.Overweight","plc110.VesiK.Pereves"), //Весы K.Вес не норма
//new Tag("ScalesK_CurReal","MixRubber17.PLC100_ScalesK.CurReal","plc110.VesiK.FullReal"), //Бункер.Измеренный вес
//new Tag("ScalesK_FullReal","MixRubber17.PLC100_ScalesK.FullReal","plc110.VesiK.FullReal"), //Весы K.Общий измеренный вес
// new Tag("ScalesK_KRP","MixRubber17.PLC100_ScalesK.KRP","plc110.VesiK.KRP"),//Весы K.Обработка КРП

// new Tag("ScalesK_TareIsBusy", "MixRubber17.PLC110-504.ScalesK_Status.Tare_IsBusy"),
// new Tag("ScalesK_Weight_NotNormal", "MixRubber17.PLC110-504.ScalesK_Status.Weight_NotNormal"),
// new Tag("ScalesK_Fail", "MixRubber17.PLC110-504.ScalesK_Fail.Fail"),
// new Tag("ScalesK_BatchersFail", "MixRubber17.PLC110-504.ScalesK_Fail.BatchersFail"),
// new Tag("ScalesK_Weighing", "MixRubber17.PLC110-504.ScalesK_Status.Weighing"),

// new Tag("DustCollectorOn", "MixRubber17.PLC110-504.MixRubber_Status.DustCollectorOn"),
// new Tag("MR_PressureOff", "MixRubber17.PLC110-504.MixRubber_Status.PressureOff"),
// new Tag("MR_UpperPressUp", "MixRubber17.PLC110-504.MixRubber_Status.UpperPressUp"),
// new Tag("MR_UpperPressDown", "MixRubber17.PLC110-504.MixRubber_Status.UpperPressDown"),
// new Tag("MR_FlapValveClosed", "MixRubber17.MixRubber17.PLC110-504.MixRubber_Status.FlapValveClosed"),//1-закрыта
// new Tag("MR_PinClosed", "MixRubber17.PLC110-504.MixRubber_Status.PinClosed"),
// new Tag("MR_RegimWork", "MixRubber17.PLC110-504.MixRubber_Status.RegimWork", "plc110.plc110_SCADA.XX6"), //Режим работы резиносмесителя. 0,1 - холостой;  2,3,4 - смесь; 3 - выгрузка

// new Tag("UpPress_PressureNotOff", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_PressureNotOff"),
// new Tag("UpPress_NotUp", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_NotUp"),
// new Tag("UpPress_NotDown", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_NotDown"),
// new Tag("UpPress_UpAndDown", "MixRubber17.PLC110-504.MixRubber_Fail.UpPress_UpAndDown"),
// new Tag("MR_Stoped", "MixRubber17.PLC110-504.MixRubber_Fail.MR_Stoped"),
// new Tag("Pin_NotClose", "MixRubber17.PLC110-504.MixRubber_Fail.Pin_NotClose"),
// new Tag("Pin_NotOpen", "MixRubber17.PLC110-504.MixRubber_Fail.Pin_NotOpen"),
// new Tag("FlapValve_NotOpened", "MixRubber17.PLC110-504.MixRubber_Fail.FlapValve_NotOpened"),
// new Tag("FlapValve_NotClosed", "MixRubber17.PLC110-504.MixRubber_Fail.FlapValve_NotClosed"),
// new Tag("HighTemperature", "MixRubber17.PLC110-504.MixRubber_Fail.HighTemperature"),
// new Tag("CriticalTemperature", "MixRubber17.PLC110-504.MixRubber_Fail.CriticalTemperature"),
// new Tag("RegMixing_Stoped", "MixRubber17.PLC110-504.MixRubber_Fail.RegMixing_Stoped"),
// new Tag("SensorT1_Fail", "MixRubber17.PLC110-504.MixRubber_Fail.SensorT1_Fail"),
// new Tag("SensorT2_Fail", "MixRubber17.PLC110-504.MixRubber_Fail.SensorT2_Fail"),

// new Tag("K_CommandOn", "MixRubber17.PLC110-504.Transporters.K_CommandOn"),
// new Tag("K_Working", "MixRubber17.PLC110-504.Transporters.K_Working"),
// new Tag("K_NotOn_RegLoad", "MixRubber17.PLC110-504.Transporters.K_NotOn_RegLoad"), // Тр-р К не включился от РГ
// new Tag("K_NotStoped", "MixRubber17.PLC110-504.Transporters.K_NotStoped"),//Тр-р К не остановился

// new Tag("L1_Working", "MixRubber17.PLC110-504.Transporters.L1_Working"),
// new Tag("L1_CommandOn", "MixRubber17.PLC110-504.Transporters.L1_CommandOn"),
// new Tag("L1_NotOn_RegMix1", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegMix1"),  //Тр-р Л1 не включился от РЖ_открыт нижний затвор
// new Tag("L1_NotOn_RegMix2", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegMix2"), //Тр-р Л1 не включился_нет верхнего положения поплавка

// new Tag("L1_NotOff_RegMix", "MixRubber17.PLC110-504.Transporters.L1_NotOff_RegMix"), //	Тр-р Л1 не выключился от РЖ
// new Tag("L1_NotOn_RegLoad", "MixRubber17.PLC110-504.Transporters.L1_NotOn_RegLoad"), // Тр-р Л1 не включился от РГ
// new Tag("L1_NotStoped", "MixRubber17.PLC110-504.Transporters.L1_NotStoped"), // Тр-р Л1 не остановился

// new Tag("L2_Working", "MixRubber17.PLC110-504.Transporters.L2_Working"),
// new Tag("L2_CommandOn", "MixRubber17.PLC110-504.Transporters.L2_CommandOn"),
// new Tag("L2_NotOff", "MixRubber17.PLC110-504.Transporters.L2_NotOff"),
// new Tag("L2_NotStoped", "MixRubber17.PLC110-504.Transporters.L2_NotStoped"),

// new Tag("OilPump_Working", "MixRubber17.PLC110-504.OilPump.Working"),
// new Tag("OilPump_CommandOn", "MixRubber17.PLC110-504.Oil_Pump.CommandOn"),
// new Tag("OilPump_NotOn", "MixRubber17.PLC110-504.OilPump.NotOn"),
// new Tag("OilPump_NotOff", "MixRubber17.PLC110-504.OilPump.NotOff"),
// new Tag("OilPump_NotOn_RegMix", "MixRubber17.PLC110-504.OilPump.NotOn_RegMix"),

//Information
// new Tag("Btn_StopProcess", "MixRubber17.PLC110-504.Information.StopProcess"),
// new Tag("Btn_ManualMode", "MixRubber17.PLC110-504.Information.ManualMode"),
// new Tag("Batchers_Start1", "MixRubber17.PLC110-504.Information.Batchers_Start1"),
// new Tag("Batchers_Start2", "MixRubber17.PLC110-504.Information.Batchers_Start2"),
// new Tag("BagFilter_Blow", "MixRubber17.PLC110-504.Information.BagFilter_Blow"),
// new Tag("AddScrewOff", "MixRubber17.PLC110-504.Information.AddScrewOff"),
// new Tag("Not24V", "MixRubber17.PLC110-504.Information.Not24V"),
// new Tag("RecipeNum", "MixRubber17.PLC110-504.Information.RecipeNum", "plc110.plc110_SCADA.R440_nomRZ"),
// new Tag("Clock_MR_Load", "MixRubber17.PLC110-504.Information.Clock_MR_Load","plc110.plc110_SCADA.R634_RG"),
// new Tag("Clock_MR_Mix", "MixRubber17.PLC110-504.Information.Clock_MR_Mix","plc110.plc110_SCADA.R664_RJ"),
// new Tag("Timer_MR_Load", "MixRubber17.PLC110-504.Information.Timer_MR_Load","plc110.plc110_SCADA.CV1_RG"),
// new Tag("Timer_MR_Mix", "MixRubber17.PLC110-504.Information.Timer_MR_Mix","plc110.plc110_SCADA.CV2_RJ"),
// new Tag("Batchers_Ready", "MixRubber17.PLC110-504.Information.Batchers_Ready"),
// new Tag("Filling_OnStart", "MixRubber17.PLC110-504.Information.Filling_OnStart"),
// new Tag("LastFilling_ToPlan", "MixRubber17.PLC110-504.Information.LastFilling_ToPlan"),
// new Tag("TestRegime_On", "MixRubber17.PLC110-504.Information.TestRegime_On"),
// new Tag("Resipe_IsNot", "MixRubber17.PLC110-504.Information.Resipe_IsNot"),
// new Tag("MR_Mix_IsFobiden", "MixRubber17.PLC110-504.Information.MR_Mix_IsFobiden"),
// new Tag("MR_Load_IsFobiden", "MixRubber17.PLC110-504.Information.MR_Load_IsFobiden"),
// new Tag("EmergencyButton_Pushed", "MixRubber17.PLC110-504.Information.EmergencyButton_Pushed"),
// new Tag("StopButton_Pushed", "MixRubber17.PLC110-504.Information.StopButton_Pushed"),
// new Tag("UnloadTempr_NotSet", "MixRubber17.PLC110-504.Information.UnloadTempr_NotSet"),
 
//тестовый режим
// new Tag("TestCommand_UpPressDown", "MixRubber17.PLC110-504.TestRegim.Command_UpPressDown","plc110.plc110_SCADA.B270"), //опустить верхний пресс
// new Tag("TestCommand_FlapValveClose", "MixRubber17.PLC110-504.TestRegim.Command_FlapValveClose","plc110.plc110_SCADA.B271"), // закрыть НЗ
// new Tag("Command_ScalesJOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesJOpen"), // Команда.Открыть весы Ж
// new Tag("Command_ScalesDOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesDOpen"), // Команда.Открыть весы D	
// new Tag("Command_ScalesEOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesEOpen"), // Команда.Открыть весы E
// new Tag("Command_OilPumpOn", "MixRubber17.PLC110-507.TestRegim.Command_OilPumpOn"), // Команда.Включить маслонасос
// new Tag("Command_TransporterKOn", "MixRubber17.PLC110-507.TestRegim.Command_TransporterKOn"), // Команда.Включить транспортер K
// new Tag("Command_TransporterL2On", "MixRubber17.PLC110-507.TestRegim.Command_TransporterL2On"), // Команда.Включить транспортер Л2
// new Tag("Command_TransporterL1On", "MixRubber17.PLC110-507.TestRegim.Command_TransporterL1On"), // Команда.Включить транспортер Л1
// new Tag("TemperatureIsDanger", "MixRubber17.PLC110-507.TestRegim.TemperatureIsDanger")// Температура критическая
// new Tag("TBatchersOn", "MixRubber17.PLC110-507.TestRegim.BatchersOn")// Навески включены
// new Tag("Command_PressureOff", "MixRubber17.PLC110-507.TestRegim.Command_PressureOff")// Команда давление снять
// new Tag("Command_UpPress_Down_UP", "MixRubber17.PLC110-507.TestRegim.Command_UpPress_Down_UP")// Поднять/опустить верхний пресс (B27-0)
// new Tag("Command_ScalesSHOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesSHOpen")// Открыть весы Ш
// new Tag("Command_ScalesSСOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesСOpen")// Открыть весы С
// new Tag("Command_ScrewTC_On", "MixRubber17.PLC110-507.TestRegim.Command_ScrewTC_On")// Включить шнек ТУ		
// new Tag("Command_ContainerC_Open", "MixRubber17.PLC110-507.TestRegim.Command_ContainerC_Open")// Открыть сборную	ТУ		
// new Tag("Command_ScalesYOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesYOpen")// Открыть весы У			
// new Tag("Command_ScalesJUOpen", "MixRubber17.PLC110-507.TestRegim.Command_ScalesJUOpen")// Открыть весы Ю	

		


//таблица температуры воды
// new Tag("WaterT1_Supply", "MixRubber17.PLC110-504.TableTWater.T1_Supply","plc110.plc110_analog.Tvodi1"), // Температура воды.Т1- подача	
// new Tag("WaterT2_UpLeft", "MixRubber17.PLC110-504.TableTWater.T2_UpLeft","plc110.plc110_analog.Tvodi2"), // Температура воды.Т2
// new Tag("WaterT3_DownLeft", "MixRubber17.PLC110-504.TableTWater.T3_DownLeft","plc110.plc110_analog.Tvodi3"), //Температура воды.Т3	
// new Tag("WateT4_UpRight", "MixRubber17.PLC110-504.TableTWater.T4_UpRight","plc110.plc110_analog.Tvodi4"), //Температура воды.Т4					
// new Tag("WateT5_DownRight", "MixRubber17.PLC110-504.TableTWater.T5_DownRight","plc110.plc110_analog.Tvodi5"), //Температура воды.Т5						
// new Tag("WateRotor1", "MixRubber17.PLC110-504.TableTWater.Rotor1", "plc110.plc110_analog.Tv_rot1_6_"), //Температура воды.ротор1			
// new Tag("WateRotor2", "MixRubber17.PLC110-504.TableTWater.Rotor2", "plc110.plc110_analog.Tv_rot2_7_"), //Температура воды.ротор2

//стринговые переменные		
//new Tag("OperatorFIO", "MixRubber17.PLC110-504.StringValues.FIO","plc110.plc110_string.FIO"), //ФИО оператора					
// new Tag("RecipeName", "MixRubber17.PLC110-504.StringValues.RecipeName","plc110.plc110_string.Name_rez"), //Имя рецепта					
// new Tag("RegimeName", "MixRubber17.PLC110-504.StringValues.RegimeName", "plc110.plc110_string.Name_rej"), //Режим.Имя		
// new Tag("ShiftName", "MixRubber17.PLC110-504.StringValues.ShiftName", "plc110.plc110_string.NameSmena"), //Смена.Имя

/////Группы тегов в Lectus: Монитор,ARMO порт 505
// new Tag("ShiftNum","MixRubber17.PPLC110-505.ShiftNum","plc110.plc110_Monitor.R436_bykva"), //Номер выбранной смены A-1, Б-2, В-3, Г-4	
// new Tag("NumReg","MixRubber17.PLC110-505.NumReg","plc110.plc110_Monitor.R438_nomRJ"),//Номер загруженного режима в ПЛК
// new Tag("Tfact","MixRubber17.PLC110-505.TFact","plc110.plc110_Monitor.R649_Tfakt"),//Температура с датчика в момент выгрузки
// new Tag("TUnloading_WithCorrect","MixRubber17.PLC110-505.TUnloading_WithCorrect","plc110.plc110_Monitor.R655_Tv_P"),//Температура выгрузки заданная минус поправка
// new Tag("TDanger","MixRubber17.PLC110-505.TDanger","plc110.plc110_Monitor.R656_Tk_P"),//Температура критическая  минус поправка
// new Tag("FullTimeMix","MixRubber17.PLC110-505.FullTimeMix","plc110.plc110_Monitor.CV5_RJ"),//Время смешения полное от пуска РЖ
// new Tag("TimeMixUnderPress","MixRubber17.PLC110-505.TimeMixUnderPress","plc110.plc110_Monitor.CV6_d"),//Время смешения под давлением
// new Tag("NumLoading_Shift","MixRubber17.PLC110-505.NumLoading_Shift","plc110.plc110_Monitor.CV8_sm"),//Номер заправки от начала смены
// new Tag("NumLoading_Recept","MixRubber17.PLC110-505.NumLoading_Recept","plc110.plc110_Monitor.CV7_rej"),//Номер заправки текущего рецепта
// new Tag("TSensor1_Up","MixRubber17.PLC110-505.TSensor1_Up","plc110.plc110_Monitor.Temp1"),//Температура смеси текущая с датчика 1 (верхняя термопара) 
// new Tag("TSensor2_Down","MixRubber17.PLC110-505.TSensor2_Down","plc110.plc110_MonitorTemp2"),//Температура смеси текущая с датчика 2 (нижняя термопара) 
// new Tag("Power","MixRubber17.PLC110-505.Power","plc110.plc110_Monitor.Power"),//Ток текущий с датчика (мощщность)	 
// new Tag("PressUp_Position","MixRubber17.PLC110-505.PressUp_Position","plc110.plc110_Monitor.Рмм"),//Положение верхнего пресса в мм
// new Tag("Plan_Loadings","MixRubber17.PLC110-505.Plan_Loadings","plc110.plc110_ARMO.R439_plan"),//План заправок
// new Tag("TUnloading","MixRubber17.PLC110-505.TUnloading","plc110.plc110_ARMO.R441_Tzad"),//Температура выгрузки заданная минус поправка
// new Tag("TCorrect","MixRubber17.PLC110-505.TCorret","plc110.plc110_ARMO.R442_Pop"),//Поправка температуры заданная
// new Tag("TDanger","MixRubber17.PLC110-505.TDanger","plc110.plc110_ARMO.R443_Tkri"),//Температура критическая  заданная
// new Tag("RegMix_IsGoing","MixRubber17.PLC110-505.RegMix_IsGoing","plc110.plc110_ARMO.Zapret"),//Идет цикл смешения, запрет загрузки	
// new Tag("RegMix_On","MixRubber17.PLC110-505.RegMix_On","plc110.plc110_ARMO.Zagl_100"),//Пуск режима смешения (1 -включен)

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
