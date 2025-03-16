export function getTimeZoneInfo() {
    const { timeZone, locale } = Intl.DateTimeFormat().resolvedOptions();
    const timeZoneOffset = new Date().getTimezoneOffset();

    return {
        timeZone, 
        locale,
        timeZoneOffset
    }
}