## 1.4.2 (Oct 2, 2014)
- `caption` and `display` can be fully customized by passing a callback function.
  (Thanks iannos for suggestion)
- Added `captionTooltip` and `displayTooltip` that make use of jQuery UI tooltip for generate tooltip on caption or column header text.
  (Thanks iannos for suggestion)
- Added `sectionClasses` that can specify CSS classes to different table section.
  (Thanks iannos for suggestion)
- Added `emptyCriteria` option to identify a row is empty. Also, `isRowEmpty` and `removeEmptyRows` methods are added that make use of this changes.
  (Thanks iannos for suggestion)
- Added support on generating `select` element with `optgroup` support. You can also specify the `title` attribute for each option.
  (Thanks iannos for suggestion)
- Added `rowDataLoaded` callback function that will be useful when loading data into a row with controls with dynamic content, such as cascading drop down list.

## 1.4.1 (Aug 10, 2014)

- Added `afterRowDragged` callback function to be triggered after grid row dragged.
  (Thanks for PolarbearDK's work)
- Added `nameFormatter` callback function for format the HTML name of generated controls.
  (Thanks iannos for suggestion)

## 1.4.0 (Jul 16, 2014)

- Added `invisible` option and `showColumn` / `hideColumn` / `isColumnInvisible` methods for changing / checking visibility of columns.
- Added `resizable` option that allow column resizable.
- Added `getColumn` method to get columns array used for grid initialization.
- [Important] You are allowed to call `appendGrid` methods by passing ONE element in jQuery object only. It will not be affected if you are already using ID, such as $("#tblAppendGrid"), as jQuery selector.

## 1.3.6 (Jun 28, 2014)

- Added `getCellCtrl` and `getCellCtrlByUniqueIndex` methods for retrieving generated control for further customization.

## 1.3.5 (Apr 23, 2014)

- Added `customGridButtons` option for customize the standard grid buttons (append/remove last/insert/remove/move up/move down).
  (Thanks pixelwiz for suggestion)
- Fixed a problem that the selection of `select` elements are not correct when dragging rows.
  (Thanks pixelwiz for reporting)

## 1.3.4 (Mar 27, 2014)

- Renamed `rowBottonsInFront` option to `rowButtonsInFront`.
  (Thanks artiaggarwal for reporting)

## 1.3.3 (Mar 26, 2014)

- Added `rowBottonsInFront` option to allow generating row buttons in the front of input columns.
  (Thanks artiaggarwal for suggestion)

## 1.3.2 (Mar 08, 2014)

- Added `headerSpan` to allow header cell column span.
  (Thanks Rajeevgandhi for suggestion)
- Allowed to pass array of data to `appendRow` and `insertRow` method.
  (Thanks mailivore for suggestion)
- Fixed a problem that empty row message will not be displayed when passing empty array to `load` method.
  (Thanks mailivore for reporting)

## 1.3.1 (Jan 26, 2014)

- Removed `firstCellWidth` and `lastCellWidth` parameters
- Added `customFooterButtons` option that allow adding extra buttons to the bottom of grid
- The last column will not be displayed if all buttons are hidden

## 1.3.0 (Jan 21, 2014)

- Added `hideButtons` option that allow hiding each buttons individually and removed `hideMoveUpDown` option
- Added `customRowButtons` option that allow adding extra buttons at the last cell of each row
- Added `hideRowNumColumn` option that allow to not generating the row number column
- Added a message to be displayed when the grid is empty 
  (Thanks mailivore for above ideas)

## 1.2.3 (Jan 16, 2014)

- Fixed a problem on setting default value on `hidden` type columns (Thanks mailivore for reporting)

## 1.2.2 (Jan 02, 2014)

- Fixed a problem on serializing `hidden` type columns (Thanks ahmedsaleh747 for reporting)

## 1.2.1 (Dec 23, 2013)

- Added `buttonClasses` initial parameters (Thanks suggestion from twinh)

## 1.2.0 (Nov 15, 2013)

- Added drag & drop support
- Added `rowDragging` and `hideMoveUpDown` initial parameters

## 1.1.3 (Oct 29, 2013)

- Fixed a problem that `i18n` parameter not working even correct values passed in (Thanks DinkoMiletic for reporting)

## 1.1.2 (May 19, 2013)

- Fixed a problem on generating `ui-datepicker` columns (Thanks kado for reporting)

## 1.1.0 (April 16, 2013)

- Added support to HTML5 input type such as date/time/email
- Added `ctrlProp` column parameter

## 1.0.0 (April 10, 2013)

- Initial release