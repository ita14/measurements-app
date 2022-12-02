import { format } from 'date-fns';

export const formatDate = (date: Date | undefined): string => {
  if (date === undefined) {
    return '-';
  }

  return format(date, 'dd.MM.yyyy');
};
