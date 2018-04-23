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
using DatePeriodEdit;

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
