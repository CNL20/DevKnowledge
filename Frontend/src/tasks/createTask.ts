export type Task = {
  id: string
  name: string
  description: string
}

export function createTask(
  tasks: Task[],
  name: string,
  description = '',
): Task {
  const trimmedName = name.trim()

  if (!trimmedName) {
    throw new Error('Tên công việc là bắt buộc.')
  }

  const task: Task = {
    id: crypto.randomUUID(),
    name: trimmedName,
    description: description.trim(),
  }

  tasks.push(task)
  return task
}
