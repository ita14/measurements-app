import { toast, ToastOptions } from 'react-toastify';

const defaults: ToastOptions = {
  position: 'bottom-right',
  autoClose: 2000,
  hideProgressBar: true,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  progress: undefined
};

const NotificationService = {
  success: (text: string, options?: Partial<ToastOptions>): void => {
    toast.success(text, { ...defaults, ...options });
  },

  warning: (text: string, options?: Partial<ToastOptions>): void => {
    toast.warning(text, { ...defaults, ...options });
  },

  error: (text: string, options?: Partial<ToastOptions>): void => {
    toast.error(text, { ...defaults, ...options });
  },

  info: (text: string, options?: Partial<ToastOptions>): void => {
    toast.info(text, { ...defaults, ...options });
  }
};

export default NotificationService;
