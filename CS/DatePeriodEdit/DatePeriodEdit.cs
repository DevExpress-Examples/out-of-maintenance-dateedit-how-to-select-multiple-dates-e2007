using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using System.ComponentModel;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils.Serializing;
using System.Globalization;
using DevExpress.Utils;

namespace DatePeriodEdit_NS
{
    [UserRepositoryItem("RegisterDatePeriodEdit")]
    public class RepositoryItemDatePeriodEdit : RepositoryItemDateEdit
    {
        OptionsSelection optionsSelections;
        StoreMode periodsStoreMode;
        char separatorChar = ',';
        static RepositoryItemDatePeriodEdit() { RegisterDatePeriodEdit(); }
        public RepositoryItemDatePeriodEdit()
        {
            optionsSelections = new OptionsSelection();
            TextEditStyle = TextEditStyles.DisableTextEditor;
        }
        public const string DatePeriodEditName = "DatePeriodEdit";
        public override string EditorTypeName { get { return DatePeriodEditName; } }
        public static void RegisterDatePeriodEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(DatePeriodEditName,
              typeof(DatePeriodEdit), typeof(RepositoryItemDatePeriodEdit),
              typeof(DateEditViewInfo), new ButtonEditPainter(), true));
        }
        [Description("Gets or sets how the editor store periods selected via the calendar ."), Category(CategoryName.Format), DefaultValue(StoreMode.Default)]
        public virtual StoreMode PeriodsStoreMode
        {
            get { return periodsStoreMode; }
            set
            {
                if (PeriodsStoreMode == value) return;
                this.periodsStoreMode = value;
            }
        }
        [Description("Gets or sets the character separating periods"), Category(CategoryName.Format), DefaultValue(',')]
        public virtual char SeparatorChar
        {
            get { return this.separatorChar; }
            set
            {
                if (SeparatorChar == value) return;
                this.separatorChar = value;
                OnPropertiesChanged();
            }
        }
        [Browsable(false)]
        public override DevExpress.XtraEditors.Mask.MaskProperties Mask { get { return base.Mask; } }
        [Browsable(false)]
        public override FormatInfo EditFormat { get { return base.DisplayFormat; } }
        [Browsable(false)]
        public new DefaultBoolean VistaEditTime { get { return base.VistaEditTime; } }
        [Browsable(false)]
        public new DefaultBoolean VistaDisplayMode { get { return base.VistaDisplayMode; } }
        [Browsable(false)]
        public new string EditMask { get { return base.EditMask; } }
        [Description("Provides access to the settings used to selection."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OptionsSelection OptionsSelection { get { return optionsSelections; } }
        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            RepositoryItemDatePeriodEdit source = item as RepositoryItemDatePeriodEdit;
            this.optionsSelections = source.OptionsSelection;
            this.separatorChar = source.SeparatorChar;
            this.periodsStoreMode = source.PeriodsStoreMode;
        }
        protected override bool IsNullValue(object editValue)
        {
            if (editValue is PeriodsSet)
                return ((PeriodsSet)editValue).Periods.Count == 0;
            if (editValue is string)
            {
                PeriodsSet set = PeriodsSet.Parse((string)editValue);
                if (set != null)
                    return set.Periods.Count == 0;
            }
            return false;
        }
        public override string GetDisplayText(FormatInfo format, object editValue)
        {
            string displayText = string.Empty;
            PeriodsSet set = editValue as PeriodsSet;
            if (set != null)
                displayText = set.ToString(format.FormatString, SeparatorChar);
            else
                if (editValue is string)
                    displayText = PeriodsSet.Parse((string)editValue).ToString(format.FormatString, SeparatorChar);
            CustomDisplayTextEventArgs e = new CustomDisplayTextEventArgs(editValue, displayText);
            if (format != EditFormat)
                RaiseCustomDisplayText(e);
            return e.DisplayText;
        }

    }
    public class DatePeriodEdit : DateEdit
    {
        static DatePeriodEdit() { RepositoryItemDatePeriodEdit.RegisterDatePeriodEdit(); }
        public DatePeriodEdit()
            : base()
        {
            EditValue = new PeriodsSet();
        }
        public override object EditValue
        {
            get
            {
                PeriodsSet value = base.EditValue as PeriodsSet;
                if (Properties.PeriodsStoreMode == StoreMode.String)
                {
                    if (value != null) return value.ToString();
                    else return string.Empty;
                }
                else
                {
                    if (value != null) return value;
                    else return new PeriodsSet();
                }
            }
            set
            {
                if (value is PeriodsSet)
                {
                    base.EditValue = value;
                    return;
                }
                if (value is string)
                {
                    base.EditValue = PeriodsSet.Parse((string)value);
                    return;
                }
                base.EditValue = value;
            }
        }
        public override string EditorTypeName { get { return RepositoryItemDatePeriodEdit.DatePeriodEditName; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemDatePeriodEdit Properties { get { return base.Properties as RepositoryItemDatePeriodEdit; } }
        protected override PopupBaseForm CreatePopupForm() { return new VistaPopupDatePeriodEditForm(this); }
        protected override object ExtractParsedValue(ConvertEditValueEventArgs e) { return e.Value; }
    }
    public class VistaPopupDatePeriodEditForm : VistaPopupDateEditForm
    {
        public VistaPopupDatePeriodEditForm(DateEdit ownerEdit) : base(ownerEdit) { }
        protected override DateEditCalendar CreateCalendar()
        {
            VistaDateEditCalendar c = new VistaDatePeriodEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue);
            c.OkClick += new EventHandler(OnOkClick);
            return c;
        }
        public override object ResultValue
        {
            get
            {
                return Calendar.TotalPeriods.GetCopy();
            }
        }
        public new virtual VistaDatePeriodEditCalendar Calendar { get { return base.Calendar as VistaDatePeriodEditCalendar; } }
    }
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
        { if(isNeedDateChanged)
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
        protected override void DecView() { ViewLevelDown(); }
        protected virtual void onWeekNuberClick(CalendarHitInfo hit)
        {
            DayNumberCellInfo week = hit.HitObject as DayNumberCellInfo;
            if (week != null && Properties.OptionsSelection.MultiselectBehaviour != MultiselectBehaviour.Disabled )
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
    public class VistaDatePeriodEditInfoArgs : VistaDateEditInfoArgs
    {
        public VistaDatePeriodEditInfoArgs(DateEditCalendarBase calendar) : base(calendar) { }
        
        protected override bool IsMultiselectDateSelected(DayNumberCellInfo cell)
        {
            bool selected = base.IsMultiselectDateSelected(cell);
            CustomDayNumberCellInfo patchCell = cell as CustomDayNumberCellInfo;
            if (patchCell != null)
            {
                DateTime end = Calendar.CalcPeriodEndDateTime(cell.Date, Calendar.ViewLevel);
                patchCell.Marked = Calendar.TotalPeriods.ContainPeriod(cell.Date, end);
                patchCell.ContainMark = Calendar.TotalPeriods.ContainPartOfPeriod(cell.Date, end);

                if (selected)
                {
                    if (Calendar.Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Merging || 
                        (Calendar.Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Intersection && patchCell.Marked == true))
                        patchCell.ContainMark = false;
                    if (patchCell.Marked && Calendar.Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Intersection)
                         selected = false;
                    patchCell.Marked = false;
                }
            }
            return selected;
        }
        protected override bool IsDateActive(DayNumberCellInfo cell)
        {
            if (Calendar.ViewLevel == ViewLevel.Weeks) return true;
            return base.IsDateActive(cell);
        }
        protected override void CalcItemsInfo()
        {
            if (Calendar.ViewLevel == ViewLevel.Weeks)
                CalcWeekItemsInfo();
            else
                base.CalcItemsInfo();
        }
        protected virtual void CalcWeekItemsInfo()
        {
            DayCells.Clear();
            WeekCells.Clear();
            Rectangle rect = new Rectangle(new Point(DateClientRect.X + DistanceFromLeftToCell, DateClientRect.Y), new Size((DateClientRect.Width - 4) / 2, DateClientRect.Height / 3));
            DayNumberCellInfo currInfo;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    currInfo = CreateWeekCellInfo(row, col);
                    if (currInfo != null)
                    {
                        currInfo.SetAppearance(Appearance);
                        currInfo.TextBounds = CalcCellTextRect(currInfo.Text, rect);
                        currInfo.Bounds = rect;
                        DayCells.Add(currInfo);
                    }
                    rect.Offset(rect.Width, 0);
                }
                rect.X = DateClientRect.X + DistanceFromLeftToCell;
                rect.Offset(0, rect.Height);
            }
            UpdateExistingCellsState();
        }
        protected virtual DayNumberCellInfo CreateWeekCellInfo(int row, int col)
        {
            DayNumberCellInfo currInfo;
            DateTime dt = FirstVisibleDate.AddDays(14 * row + 7 * col);
            currInfo = new CustomDayNumberCellInfo(dt);
            DateTime endDay = currInfo.Date.AddDays(6);
            string dateSeparator = " ";
            currInfo.Text = Calendar.DateFormat.GetAbbreviatedMonthName(currInfo.Date.Month) + dateSeparator + currInfo.Date.Day + " - " +
                Calendar.DateFormat.GetAbbreviatedMonthName(endDay.Month) + dateSeparator + endDay.Day;
            return currInfo;
        }
        public new virtual VistaDatePeriodEditCalendar Calendar { get { return base.Calendar as VistaDatePeriodEditCalendar; } }
        protected override DayNumberCellInfo CreateDayCell(DateTime date) { return new CustomDayNumberCellInfo(date); }
        protected override DayNumberCellInfo CreateMonthCellInfo(int row, int col)
        {
            DayNumberCellInfo oldInfo;
            oldInfo = base.CreateMonthCellInfo(row, col);
            if (oldInfo == null) return oldInfo;
            CustomDayNumberCellInfo patchedInfo = new CustomDayNumberCellInfo(oldInfo.Date);
            patchedInfo.Text = oldInfo.Text;
            return patchedInfo;
        }
        protected override DayNumberCellInfo CreateYearCellInfo(int row, int col)
        {
            DayNumberCellInfo oldInfo;
            oldInfo = base.CreateYearCellInfo(row, col);
            if (oldInfo == null) return oldInfo;
            CustomDayNumberCellInfo patchedInfo = new CustomDayNumberCellInfo(oldInfo.Date);
            patchedInfo.Text = oldInfo.Text;
            return patchedInfo;
        }
        public override CalendarHitInfo GetHitInfo(MouseEventArgs e)
        {
            CalendarHitInfo baseHitInfo = base.GetHitInfo(e);
            if (baseHitInfo.InfoType != CalendarHitInfoType.Unknown) return baseHitInfo;

            if (OkButtonRect.Contains(e.Location))
            {
                baseHitInfo.InfoType = CalendarHitInfoType.Ok;
                baseHitInfo.Bounds = OkButtonRect;
            }
            else if (CancelButtonRect.Contains(e.Location))
            {
                baseHitInfo.InfoType = CalendarHitInfoType.Cancel;
                baseHitInfo.Bounds = CancelButtonRect;
            }
            if (ShowWeekNumbers)
                for (int i = 0; i < WeekCells.Count; i++)
                    if (WeekCells[i].Bounds.Contains(e.Location))
                    {
                        DateTime date = new DateTime(DayCells[0].Date.Year, DayCells[0].Date.Month, DayCells[0].Date.Day, 0, 0, 0);
                        date = date.AddDays(7 * i);
                        DayNumberCellInfo cell = new DayNumberCellInfo(date);
                        baseHitInfo.InfoType = CalendarHitInfoType.WeekNumber;
                        baseHitInfo.HitObject = cell;
                    }
            return baseHitInfo;
        }
        protected override void CalcHeaderInfo()
        {
            base.CalcHeaderInfo();
            int indent = GetButtonRect(Rectangle.Empty).Width / 2;
            ClearRect = new Rectangle(LeftArrowInfo.Bounds.Right + indent, Content.Bottom + IndentFromDateInfoToClearText, ClearRect.Width, ClearRect.Height);
            OkRect = new Rectangle(LeftArrowInfo.Bounds.Right + (RightArrowInfo.Bounds.X - LeftArrowInfo.Bounds.Right - OkRect.Width) / 2, Content.Bottom + IndentFromDateInfoToClearText, OkRect.Width, OkRect.Height);
            CancelRect = new Rectangle(RightArrowInfo.Bounds.X - indent - CancelRect.Right, Content.Bottom + IndentFromDateInfoToClearText, CancelRect.Width, CancelRect.Height);
            OkButtonRect = GetButtonRect(OkRect);
            CancelButtonRect = GetButtonRect(CancelRect);
            ClearButtonRect = GetButtonRect(ClearRect);
        }
    }
    public class VistaDatePeriodEditPainter : VistaDateEditPainter
    {
        public VistaDatePeriodEditPainter(DateEditCalendarBase calendar) : base(calendar) { }
        protected override void DrawDayCell(CalendarObjectInfoArgs info, DayNumberCellInfo cell)
        {
            bool isDrawn = false;
            CustomDayNumberCellInfo patchCell = cell as CustomDayNumberCellInfo;
            if (patchCell != null) isDrawn = DrawPatchedCell(info, patchCell);
            if (!isDrawn) base.DrawDayCell(info, cell);
        }
        protected virtual bool DrawPatchedCell(CalendarObjectInfoArgs info, CustomDayNumberCellInfo cell)
        {
            cell.Today = cell.Marked;
            base.DrawDayCell(info, cell);
            if (!cell.Marked)
                if (cell.ContainMark)
                    MarkCellContent(info, cell);
            return true;
        }
        protected override void DrawWeekdaysAbbreviation(CalendarObjectInfoArgs info)
        {
            if (((VistaDatePeriodEditCalendar)info.Calendar).ViewLevel == ViewLevel.Weeks) return;
            base.DrawWeekdaysAbbreviation(info);
        }
        protected virtual void MarkCellContent(CalendarObjectInfoArgs info, DayNumberCellInfo cell)
        {
            int width = cell.Bounds.Width / 3, height = cell.Bounds.Height / 3;
            Rectangle r = new Rectangle(cell.Bounds.Location, new Size(width, height));
            r.Offset(width * 2, height * 2);
            DayNumberCellInfo icon = new DayNumberCellInfo(cell.Date);
            icon.Today = true;
            icon.Bounds = r;
            icon.Text = string.Empty;
            icon.Selected = true;
            base.DrawDayCell(info, icon);
        }
        protected override void DrawHeader(CalendarObjectInfoArgs info)
        {
            base.DrawHeader(info);
            VistaDateEditInfoArgs vdi = info as VistaDateEditInfoArgs;
            if (vdi == null) return;
            DrawOk(vdi);
            DrawCancel(vdi);
        }
    }
    public class VistaDatePeriodEditCalendarSelectState : VistaDateEditCalendarSelectState
    {
        public VistaDatePeriodEditCalendarSelectState(DateEditCalendarBase control) : base(control) { }
        public virtual VistaDatePeriodEditCalendar DatePeriodCalendar { get { return VistaCalendar as VistaDatePeriodEditCalendar; } }
        protected override void UpdateSelection(MouseEventArgs e)
        {
            if (DatePeriodCalendar.Properties.OptionsSelection.MultiselectBehaviour == MultiselectBehaviour.Disabled ) return;
            int oldSelectionCount = DatePeriodCalendar.Selection.Count;
            base.UpdateSelection(e);
            if (oldSelectionCount != DatePeriodCalendar.Selection.Count && DatePeriodCalendar.Selection.Count == 0)
            {
                DatePeriodCalendar.UpdateSelection();
            }
        }
        protected override bool ShiftKeyPressed { get { return false; } }
        protected override void FindMinMaxDateInRect(Rectangle rect, ref DateTime minDate, ref DateTime maxDate, bool inverse)
        {
            Point down, up;
            if (inverse)
            {
                down = new Point(rect.Left, rect.Bottom);
                up = new Point(rect.Right, rect.Top);
            }
            else
            {
                up = rect.Location;
                down = new Point(rect.Right, rect.Bottom);
            }
            DayNumberCellInfo minCell, maxCell;
            minCell = GetCellByPoint(down, false);
            maxCell = GetCellByPoint(up, false);
            minDate = DateTime.MaxValue;
            maxDate = DateTime.MinValue;
            if (minCell != null && maxCell != null)
            {
                if (maxCell != minCell)
                {
                    if (minCell.Date < maxCell.Date)
                    {
                        minDate = minCell.Date;
                        maxDate = maxCell.Date;
                    }
                    else
                    {
                        maxDate = minCell.Date;
                        minDate = maxCell.Date;
                    }
                }
            }
        }
        protected override DayNumberCellInfo GetCellByPoint(Point pt, bool nearestLeft)
        {
            foreach (DayNumberCellInfo cell in DatePeriodCalendar.GetDayCells())
                if (cell.Bounds.Contains(pt)) return cell;
            return null;             
        }
    }
    public class CustomDayNumberCellInfo : DayNumberCellInfo
    {
        bool marked;
        bool containMark;
        public CustomDayNumberCellInfo(DateTime date)
            : base(date)
        {
            marked = false;
            containMark = false;
        }
        public bool Marked { get { return marked; } set { marked = value; } }
        public bool ContainMark { get { return containMark; } set { containMark = value; } }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OptionsSelection
    {
        MultiselectBehaviour multiselectBehaviour;
        ViewLevel lowLevel, highLevel, defaultLevel;
        bool showWeekLevel;
        public OptionsSelection()
        {
            multiselectBehaviour = MultiselectBehaviour.Merging;
            lowLevel = ViewLevel.Days;
            highLevel = ViewLevel.Years;
            defaultLevel = ViewLevel.Days;
            showWeekLevel = false;
        }
        [Description("Allow chose multiselection behaviour."), Category(CategoryName.Properties), DefaultValue(MultiselectBehaviour.Merging)]
        public MultiselectBehaviour MultiselectBehaviour { get { return multiselectBehaviour; } set { multiselectBehaviour = value; } }
        [Description("Allow chose weather week level will be shown."), Category(CategoryName.Properties), DefaultValue(false)]
        public bool ShowWeekLevel
        {
            get { return showWeekLevel; }
            set
            {
                showWeekLevel = value;
            }
        }
        [Description("Allow chose the lowest navigation level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Days)]
        public ViewLevel LowLevel
        {
            get { return lowLevel; }
            set
            {
                lowLevel = value;
            }
        }
        [Description("Allow chose the higest navigation level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Years)]
        public ViewLevel HightLevel
        {
            get { return highLevel; }
            set
            {
                highLevel = value;
            }
        }
        [Description("Allo chose the first shoun level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Days)]
        public ViewLevel DefaultLevel
        {
            get { return defaultLevel; }
            set
            {
                defaultLevel = value;
            }
        }
    }
    public enum MultiselectBehaviour { Merging, Intersection, Disabled }
    public enum ViewLevel { Days, Weeks, Months, Years}
    public enum StoreMode { Default, PeriodsSet, String }
}
