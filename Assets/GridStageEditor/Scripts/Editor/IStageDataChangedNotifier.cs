using System;

namespace GridStageEditor
{
    public interface IStageDataChangedNotifier
    {
        public Action<StageData> NotifyStageDataChanged { set; }
    }
}