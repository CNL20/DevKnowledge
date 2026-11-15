export function rateCompletedTask(
  taskId: string,
  stars: number,
  comment = '',
) {
  if (!Number.isInteger(stars) || stars < 1 || stars > 5) {
    throw new Error('Số sao phải từ 1 đến 5.')
  }

  return {
    taskId,
    stars,
    comment: comment.trim(),
    ratedAt: new Date().toISOString(),
  }
}
