using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DatePeriodEdit_NS;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Calendar;
using System.Windows.Forms;

namespace DatePeriodEdit
{
    public class VistaDatePeriodEditCalendar : VistaDateEditCalendar
    {
        PeriodsSet totalPeriods, totalPeriodsCopy;
        bool allowMark;
        ViewLevel viewLevel;
        public VistaDatePeriodEditCalendar(RepositoryItemDateEdit item, object editDate)
            : base(item, editDate)
        {
            Selection.Clear();
            Multiselect = true;
            PeriodsSet editValue = Properties.OwnerEdit.EditValue as PeriodsSet;
            totalPeriods = new PeriodsSet();
            viewLevel = GetNewLevel(Properties.OptionsSelection.DefaultLevel, Properties.OptionsSelection.DefaultLevel);
            CreatePrevImage(false);
        }
        bool isNeedDateChanged = true;
        public override void ResetState(object editDate, DateTime dt)
        {
            UpdateTotalPeriods(editDate);
            base.ResetState(editDate, dt);
            isNeedDateChanged = false;
            if (TotalPeriods.Periods.Count == 0)
                DateTime = DateTime.Now;
            else
                DateTime = TotalPeriods[0].Begin;
            isNeedDateChanged = true;
            ViewLevel = GetNewLevel(ViewLevel, ViewLevel);
        }

        protected override void OnDateTimeChanged(DateTime value)
        {
            if (isNeedDateChanged)
                base.OnDateTimeChanged(value);
        }
        public virtual new DateTime DateTime { get { return base.DateTime.Date; } set { base.DateTime = value.Date; } }
        protected virtual void UpdateTotalPeriods(object value)
        {
            PeriodsSet editValue = value as PeriodsSet;
            TotalPeriods.Periods.Clear();
            if (editValue != null)
                TotalPeriods = editValue.GetCopy();
            else
                if (value is string)
                    TotalPeriods = PeriodsSet.Parse((string)value);
        }
        protected virtual bool CtrlKeyPressed { get { return (System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0; } }
        protected override void OnDateTimeCommit(object value, bool cleared) { }
        protected internal virtual new RepositoryItemDatePeriodEdit Properties { get { return base.Properties as RepositoryItemDatePeriodEdit; } }
        protected internal virtual bool GetSwitchState() { return SwitchState; }
        protected override DateEditInfoArgs CreateInfoArgs() { return new VistaDatePeriodEditInfoArgs(this); }
        protected override DateEditPainter CreatePainter() { return new VistaDatePeriodEditPainter(this); }
        protected override DateEditCalendarStateBase CreateSelectionState() { return new VistaDatePeriodEditCalendarSelectState(this); }
        public virtual PeriodsSet TotalPeriods { get { return totalPeriods; } set { totalPeriods = value; } }
        protected virtual internal DayNumberCellInfoCollection GetDayCells() { return Calendars[0].DayCells; }
        protected override void OnMoveVertical(int dir) { }
        protected override void OnMoveHorizontal(int dir) { }
        protected override void SetViewCore(DateEditCalendarViewType v) { }
        public override DateEditCalendarViewType View { get { return ConvertViewLevelToView(ViewLevel); } set { } }
        protected override void SetSelection(DateTime date) { }
        protected override void SetSelectionRange(DateTime date) { }
        public virtual ViewLevel ViewLevel
        {
            get { return viewLevel; }
            set
            {
                ViewLevel newValue = GetNewLevel(value, ViewLevel);
                ViewLevel oldValue = ViewLevel;
                if (oldValue == newValue) return;
                DateEditCalendarViewType oldView, newView;
                if (oldValue == ViewLevel.Days && newValue == ViewLevel.Weeks)
                {
                    oldView = DateEditCalendarViewType.MonthInfo;
                    newView = DateEditCalendarViewType.YearInfo;
                }
                else
                {
                    oldView = ConvertViewLevelToView(oldValue);
                    newView = ConvertViewLevelToView(newValue);
                }
                OnViewChanging(oldView, newView);
                viewLevel = newValue;
                OnViewChanged(oldView, newView);
            }
        }
        protected virtual ViewLevel GetNewLevel(ViewLevel newLevel, ViewLevel currentLevel)
        {
            ViewLevel lowLevel = Properties.OptionsSelection.LowLevel;
            ViewLevel highLevel = Properties.OptionsSelection.HightLevel;
            if (!Properties.OptionsSelection.ShowWeekLevel)
            {
                if (lowLevel == ViewLevel.Weeks) lowLevel = ViewLevel.Months;
                if (highLevel == ViewLevel.Weeks) highLevel = ViewLevel.Days;
            }
            if (lowLevel > highLevel) return currentLevel;
            if (newLevel < lowLevel) return lowLevel;
            if (newLevel > highLevel) return highLevel;
            return newLevel;
        }
        public virtual void ViewLevelUp()
        {
            if (ViewLevel == ViewLevel.Days)
                if (Properties.OptionsSelection.ShowWeekLevel) ViewLevel = ViewLevel.Weeks;
                else ViewLevel = ViewLevel.Months;
            else if (ViewLevel == ViewLevel.Weeks) ViewLevel = ViewLevel.Months;
            else ViewLevel = ViewLevel.Years;
        }
        public virtual void ViewLevelDown()
        {
            if (ViewLevel == ViewLevel.Years) ViewLevel = ViewLevel.Months;
            else if (ViewLevel == ViewLevel.Months)
                if (Properties.OptionsSelection.ShowWeekLevel) ViewLevel = ViewLevel.Weeks;
                else ViewLevel = ViewLevel.Days;
            else
                ViewLevel = ViewLevel.Days;
        }

        protected virtual DateEditCalendarViewType ConvertViewLevelToView(ViewLevel level)
        {
            if (level == ViewLevel.Days) return DateEditCalendarViewType.MonthInfo;
            if (level == ViewLevel.Weeks) return DateEditCalendarViewType.MonthInfo;
            if (level == ViewLevel.Months) return DateEditCalendarViewType.YearInfo;
            if (level == ViewLevel.Years) return DateEditCalendarViewType.YearsInfo;
            return DateEditCalendarViewType.YearsInfo;
        }
        protected virtual void MarkSelectedDay()
        {
            if (Selection.Count == 0) return;
            MarkPeriod(Selection[0], Selection[1]);
        }
        protected virtual void MarkPeriod(DateTime begin, DateTime end)
        {
            if (Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Merging)
                TotalPeriods.MergeWith(begin, end);
            else if (Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Intersection)
                TotalPeriods.IntersectWith(begin, end);
            else if (Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Disabled)
            {
                if (!TotalPeriods.ContainPeriod(begin, end))
                    TotalPeriods.Periods.Clear();
                TotalPeriods.IntersectWith(begin, end);
            }
            UpdateSelection();
            Selection.Clear();
        }
        protected virtual internal void UpdateSelection()
        {
            UpdateExistingCellsState();
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CalendarHitInfo hit = GetHitInfo(e);
            totalPeriodsCopy = totalPeriods.GetCopy();
            if (!CtrlKeyPressed)
                if ((hit.InfoType == CalendarHitInfoType.Item) ||
                    hit.InfoType == CalendarHitInfoType.WeekNumber || hit.InfoType == CalendarHitInfoType.Unknown)
                    TotalPeriods.Periods.Clear();
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            allowMark = true;
            base.OnMouseUp(e);
            if (!allowMark) return;
            MarkSelectedDay();
        }
        protected override void OnItemClick(CalendarHitInfo hitInfo)
        {
            DayNumberCellInfo cell = hitInfo.HitObject as DayNumberCellInfo;
            if (cell != null)
            {
                ChangeDateOnItemClick(cell);
                if (ViewLevel == Properties.OptionsSelection.LowLevel || (CtrlKeyPressed && Properties.OptionsSelection.MultiselectBehaviour != MultiselectBehaviour.Disabled))
                {
                    MarkItemOnClick(cell);
                }
                else
                {
                    TotalPeriods = totalPeriodsCopy.GetCopy();
                    ViewLevelDown();
                }
            }
        }
        protected virtual void MarkItemOnClick(DayNumberCellInfo cell)
        {
            DateTime begin = CalcPeriodBeginDateTime(cell.Date);
            if (ViewLevel == ViewLevel.Days)
                MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel));
            else if (ViewLevel == ViewLevel.Weeks)
                MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel));
            else if (ViewLevel == ViewLevel.Months)
                MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel));
            else if (ViewLevel == ViewLevel.Years)
                MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel));
        }
        protected virtual void ChangeDateOnItemClick(DayNumberCellInfo cell)
        {

            if (viewLevel == ViewLevel.Weeks) return;
            DateTime maxDate = DateTime;
            if (cell.Date.Month != DateTime.Month)
                maxDate = cell.Date;
            else
                maxDate = CalcPeriodEndDateTime(cell.Date, ViewLevel);
            if (viewLevel < ViewLevel.Months)
            {
                if (DateTime.Month < maxDate.Month)
                    if (DateTime.Year == maxDate.Year)
                        OnRightArrowClick();
                    else
                        OnLeftArrowClick();
                else if (DateTime.Month > maxDate.Month)
                    if (DateTime.Year == maxDate.Year)
                        OnLeftArrowClick();
                    else
                        OnRightArrowClick();
                return;
            }
            if (ViewLevel == ViewLevel.Days)
                DateTime = new DateTime(cell.Date.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, cell.Date.Day), 0, 0, 0);
            else if (ViewLevel == ViewLevel.Weeks)
                DateTime = new DateTime(cell.Date.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, DateTime.Day), 0, 0, 0);
            else if (ViewLevel == ViewLevel.Months)
                DateTime = new DateTime(DateTime.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, DateTime.Day), 0, 0, 0);
            else if (ViewLevel == ViewLevel.Years)
                DateTime = new DateTime(cell.Date.Year, DateTime.Month, CorrectDay(cell.Date.Year, DateTime.Month, DateTime.Day), 0, 0, 0);
        }
        protected override void ProcessClick(CalendarHitInfo hit)
        {
            allowMark = false;
            base.ProcessClick(hit);
            if (hit.InfoType == CalendarHitInfoType.WeekNumber)
                onWeekNuberClick(hit);
        }
        protected override void IncView() { ViewLevelUp(); }
        //protected override DateEditCalendarViewType DecView() { ViewLevelDown(); }
        protected virtual void onWeekNuberClick(CalendarHitInfo hit)
        {
            DayNumberCellInfo week = hit.HitObject as DayNumberCellInfo;
            if (week != null && Properties.OptionsSelection.MultiselectBehaviour != MultiselectBehaviour.Disabled)
            {
                MarkPeriod(week.Date, week.Date.AddDays(7).AddSeconds(-1));
            }
        }
        protected override void OnClearClick()
        {
            TotalPeriods.Periods.Clear();
            UpdateExistingCellsState();
            Invalidate();
        }
        protected override void OnCancelClick()
        {
            Properties.OwnerEdit.CancelPopup();
        }
        public virtual DateTime CalcPeriodBeginDateTime(DateTime begin) { return begin.Date; }
        public virtual DateTime CalcPeriodEndDateTime(DateTime begin, ViewLevel level)
        {
            DateTime end = begin;
            if (level == ViewLevel.Days)
            {
                end = end.AddDays(1);
                end = end.AddSeconds(-1);
            }
            else if (level == ViewLevel.Weeks)
            {
                end = end.AddDays(7);
                end = end.AddSeconds(-1);
            }
            else if (level == ViewLevel.Months)
            {
                end = end.AddMonths(1);
                end = end.AddSeconds(-1);
            }
            else if (level == ViewLevel.Years)
            {
                end = end.AddYears(1);
                end = end.AddSeconds(-1);
            }
            return end;
        }
        protected override DateTime GetStartSelectionByState(DateTime date)
        {
            if (ViewLevel == ViewLevel.Weeks)
                return GetFirstDayOfTheWeek(date);
            return base.GetStartSelectionByState(date);
        }
        protected override DateTime GetEndSelectionByState(DateTime dt)
        {
            if (ViewLevel == ViewLevel.Weeks)
                return GetLastDayOfTheWeek(dt);
            return base.GetEndSelectionByState(dt);
        }
        protected virtual DateTime GetFirstDayOfTheWeek(DateTime date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            for (; dt.DayOfWeek != FirstDayOfWeek; dt = dt.AddDays(-1)) ;
            return dt;
        }
        protected virtual DateTime GetLastDayOfTheWeek(DateTime date)
        {
            DateTime dt = GetFirstDayOfTheWeek(date);
            dt = dt.AddDays(7).AddSeconds(-1);
            return dt;
        }

    }
}
