# Debugging Evidence

## BUG001 - Login Required Field Validation

### Original faulty code
```csharp
if (_users[username] == password)
{
    ShowApplication();
}
```

### Issue
The code does not check whether username and password fields are empty.

### Corrected code
```csharp
if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
{
    ShowLoginError("Username and password are required.");
    return;
}
```

## BUG002 - Invalid Login Handling

### Original faulty code
```csharp
if (_users[username] == password)
{
    ShowApplication();
}
```

### Issue
If the username does not exist, the app can crash or fail to show a proper error.

### Corrected code
```csharp
if (!_users.TryGetValue(username, out var validPassword) || validPassword != password)
{
    ShowLoginError("Invalid username or password.");
    return;
}
```

## BUG003 - Blank Student Registration

### Original faulty code
```csharp
_students.Add(new Student(number, name, email));
```

### Issue
The app could save a student with blank details.

### Corrected code
```csharp
if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
{
    MessageBox.Show("Student number, name and email are required.");
    return;
}
```

## BUG004 - Duplicate Course Enrolment

### Original faulty code
```csharp
_enrollments.Add(new Enrollment(student.StudentNumber, student.FullName, course));
```

### Issue
The same student could be enrolled in the same course more than once.

### Corrected code
```csharp
if (_enrollments.Any(e => e.StudentNumber == student.StudentNumber && e.Course == course))
{
    MessageBox.Show("This student is already enrolled for the selected course.");
    return;
}
```

## BUG005 - Incorrect Marks Validation

### Original faulty code
```csharp
var mark = decimal.Parse(_txtMark.Text);
_marks.Add(new MarkRecord(student.StudentNumber, student.FullName, course, mark));
```

### Issue
The app could crash if text such as "abc" was entered, and it did not block marks below 0 or above 100.

### Corrected code
```csharp
if (!decimal.TryParse(_txtMark.Text.Trim(), out var mark))
{
    MessageBox.Show("Mark must be a numeric value.");
    return;
}

if (mark < 0 || mark > 100)
{
    MessageBox.Show("Mark must be between 0 and 100.");
    return;
}
```
