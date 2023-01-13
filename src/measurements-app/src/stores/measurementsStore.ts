import { Interval, sub } from 'date-fns';
import { create } from 'zustand';
import { devtools } from 'zustand/middleware';

interface MeasurementsState {
  dateRange: Interval;
  setDateRange: (dateRange: Interval) => void;
}

export const useMeasurementsStore = create<MeasurementsState>()(
  devtools(
    (set) => ({
      dateRange: { start: sub(new Date(), { days: 1 }), end: new Date() },
      setDateRange: (dateRange: Interval) => set({ dateRange })
    }),
    {
      name: 'measurements-store'
    }
  )
);
