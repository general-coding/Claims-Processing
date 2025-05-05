export interface User {
  user_id: number;
  user_fname: string;
  user_mname?: string;
  user_lname: string;
  user_fullname?: string;
  user_email: string;
  user_password: string;
  created_on: Date;
  modified_on?: Date;
}

export const UserColumns = [
  { name: 'user_id', label: 'User ID', type: 'string' },
  { name: 'user_fname', label: 'First Name', type: 'string' },
  { name: 'user_mname', label: 'Middle Name', type: 'string' },
  { name: 'user_lname', label: 'Last Name', type: 'string' },
  { name: 'user_fullname', label: 'Full Name', type: 'string' },
  { name: 'user_email', label: 'Email', type: 'string' },
  { name: 'user_password', label: 'Password', type: 'string' },
  { name: 'created_on', label: 'Created On', type: 'date' },
  { name: 'modified_on', label: 'Modified On', type: 'date' },
  { name: 'isEdit', label: 'Edit', type: 'boolean' },
];
