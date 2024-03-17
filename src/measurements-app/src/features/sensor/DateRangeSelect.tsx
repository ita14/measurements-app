import React from 'react';
import { addDays, isAfter, isBefore, subDays, subMonths, subWeeks } from 'date-fns';
import { Box, TextField, ToggleButtonGroup, ToggleButton } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFnsV3';
import { useMeasurementsStore } from '../../stores/measurementsStore';

const enum Ranges {
  Day,
  Week,
  Month
}

function DateRangeSelect() {
  const { dateRange, setDateRange } = useMeasurementsStore();

  const handleDateChange = (date: string | number | Date | null, isStart = false) => {
    if (date === null) {
      return;
    }

    const newRange = isStart
      ? { start: date, end: isBefore(date, dateRange.end) ? dateRange.end : addDays(date, 1) }
      : { start: isAfter(date, dateRange.end) ? dateRange.start : subDays(date, 1), end: date };

    setDateRange(newRange);
  };

  const handleRangeSelect = (event: React.MouseEvent<HTMLElement>, newValue: Ranges) => {
    const end = new Date();
    let start;
    switch (newValue) {
      case Ranges.Day:
        start = subDays(end, 1);
        break;
      case Ranges.Week:
        start = subWeeks(end, 1);
        break;
      case Ranges.Month:
        start = subMonths(end, 1);
        break;
    }
    setDateRange({ start, end });
  };

  return (
    <Box sx={{ display: 'flex', padding: 2, ml: 2, mr: 2 }}>
      <ToggleButtonGroup color="primary" exclusive size="small" onChange={handleRangeSelect}>
        <ToggleButton value={Ranges.Day}>Past day</ToggleButton>
        <ToggleButton value={Ranges.Week}>Week</ToggleButton>
        <ToggleButton value={Ranges.Month}>Month</ToggleButton>
      </ToggleButtonGroup>

      <LocalizationProvider dateAdapter={AdapterDateFns}>
        <DatePicker
          label="Start date"
          value={dateRange?.start}
          onChange={(newValue) => {
            handleDateChange(newValue, true);
          }}
          // renderInput={(params) => <TextField {...params} sx={{ marginLeft: 'auto', mr: 1 }} />}
        />
        <DatePicker
          label="End date"
          value={dateRange?.end}
          onChange={(newValue) => {
            handleDateChange(newValue);
          }}
          // renderInput={(params) => <TextField {...params} />}
        />
      </LocalizationProvider>
    </Box>
  );
}

export default DateRangeSelect;
