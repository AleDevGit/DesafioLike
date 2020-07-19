import { Role } from './Role';
import { User } from './User';

export class UserRole {
    id: number;
    user: User[];
    role: Role[];
    userRole: UserRole[];
}
