import React from 'react';
import { differenceInCalendarDays } from 'date-fns';
import { DateRange, DateRangePicker } from 'materialui-daterange-picker';
import { formatDate } from '../utils/dates';
import { Typography, Box } from '@mui/material';

export interface Props {
  isOpen: boolean;
  dateRange: DateRange;
  onToggleOpen: () => void;
  onChange: (dateRange: DateRange) => void;
}

const MAX_ALLOWED_DAYS = 31;

function DateRangeSelect({ isOpen, dateRange, onChange, onToggleOpen }: Props) {
  const handleDateChange = (range: DateRange) => {
    if (range.startDate === undefined || range.endDate === undefined) {
      return;
    }

    const diff = differenceInCalendarDays(range.endDate, range.startDate);

    if (diff > MAX_ALLOWED_DAYS) {
      // TODO: show error
      window.alert('Maximum 31 days can be selected.');
      return;
    }

    onChange(range);
  };

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
      <Typography variant="h6">Time range</Typography>
      <div onClick={onToggleOpen}>
        <Typography>
          {formatDate(dateRange.startDate)} - {formatDate(dateRange.endDate)}
        </Typography>
      </div>
      <DateRangePicker
        open={isOpen}
        onChange={handleDateChange}
        toggle={onToggleOpen}
      />
    </Box>
  );
}

export default DateRangeSelect;
