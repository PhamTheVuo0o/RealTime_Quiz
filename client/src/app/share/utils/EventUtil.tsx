import { RefObject, useEffect } from 'react';
export const EventUtil = {
  useBackgroudJob: (intervalInSeconds:number, callback: () => void) => {
    useEffect(() => {
      const job = () => {
        callback();
      };
  
      // Set up the interval
      const intervalId = setInterval(job, intervalInSeconds * 1000);
      return () => clearInterval(intervalId);
    }, [intervalInSeconds, callback]);
  },
  useClickOutside: (ref: RefObject<HTMLElement>, callback: () => void, bodyElement?: HTMLElement) => {
    useEffect(() => {
      const handleClickOutside = (event: MouseEvent) => {
        if (ref.current && !ref.current.contains(event.target as Node)) {
          callback();
        }
      };

      if (bodyElement) bodyElement.addEventListener('click', handleClickOutside);

      return () => {
        if (bodyElement) bodyElement.removeEventListener('click', handleClickOutside);
      };
    }, [ref, callback]);
  },
}