export function scheduleTaskReminder(
  taskId: string,
  remindAt: string,
) {
  return {
    taskId,
    remindAt,
    status: 'scheduled',
    notificationType: 'email',
  }
}
