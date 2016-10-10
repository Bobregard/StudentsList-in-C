#include <iostream>

using namespace std;

struct student {
	int id;
	char firstName[21], middleName[21], lastName[21], pin[11];
	struct student *next;
};

struct student *head = NULL;
struct student *current = NULL;

void Add();
void DisplayClass();
void Sort();
void SearchByName(student *first, char key[]);
void SearchByPin(student *first, char key[]);

void main() {
	int a, b;
	char name[21], pin[11];

	do {
		cout << "Press 1 to add a student" << endl;
		cout << "Press 2 to display student class" << endl;
		cout << "Press 3 to search students" << endl;
		cout << "Press 4 to exit" << endl;
		cin >> a;
		switch (a) {
		case 1:
			Add();
			Sort();
			break;
		case 2:
			cout << endl << "Student class: " << endl << endl;			
			DisplayClass();
			break;
		case 3:
			do {
				cout << "Press 1 to search by name" << endl;
				cout << "Press 2 to search by PIN" << endl;
				cout << "Press 3 to go back" << endl;
				cin >> b;
				switch (b) {
				case 1:
					cout << "Enter name or part of name: ";
					cin >> name;
					SearchByName(head, name);
					break;
				case 2:
					cout << "Enter PIN: ";
					cin >> pin;
					SearchByPin(head, pin);
					break;
				default:
					break;
				}
			}
			while(b != 3);
		default:
			break;
		}
	}
	while (a != 4);
	
	while (current != NULL) {
		free(current);
		current = current->next;
	}
	free(head);
}

void Add() {
	struct student *st = (struct student *) malloc(sizeof(struct student));

	if (head == NULL) {
		head = current = st;
	}
	else {
		current = head;
		while (current->next != NULL) {
			current = current->next;
		}
		current->next = st;
		current = st;
	}

	cout << "First name: ";
	cin >> current->firstName;
	cout << "Middle name: ";
	cin >> current->middleName;
	cout << "Last name: ";
	cin >> current->lastName;
	cout << "PIN: ";
	cin >> current->pin;

	current->next = NULL;
}

void DisplayClass() {

	if (head == NULL) {
		cout << "There are no students to display." << endl << endl;
	}
	else {
		current = head;
		do {
			cout << current->id << " " << current->firstName << " " << current->middleName << " "
				<< current->lastName << " " << current->pin << endl;
			current = current->next;
		}
		while (current != NULL);
		cout << endl;
	}
}

void Sort() {
	student *nextSt, *prev, *end, *temp;
	end = NULL;
	int counter = 1;

	while (end != head->next) {
		prev = current = head;
		nextSt = current->next;

		while (current != end) {
			if (strcmp(current->firstName, nextSt->firstName) > 0) {
				temp = nextSt->next;
				nextSt->next = current;
				current->next = temp;
				if (current == head) {
					head = nextSt;
					prev = nextSt;
				}
				else {
					prev->next = nextSt;
					prev = nextSt;
				}
			}
			else if (strcmp(current->firstName, nextSt->firstName) == 0) {
				if (strcmp(current->middleName, nextSt->middleName) > 0) {
					temp = nextSt->next;
					nextSt->next = current;
					current->next = temp;
					if (current == head) {
						head = nextSt;
						prev = nextSt;
					}
					else {
						prev->next = nextSt;
						prev = nextSt;
					}
				}
				else if (strcmp(current->middleName, nextSt->middleName) == 0) {
					if (strcmp(current->lastName, nextSt->lastName) > 0) {
						temp = nextSt->next;
						nextSt->next = current;
						current->next = temp;
						if (current == head) {
							head = nextSt;
							prev = nextSt;
						}
						else {
							prev->next = nextSt;
							prev = nextSt;
						}
					}
					else {
						prev = current;
						current = current->next;
					}
				}
				else {
					prev = current;
					current = current->next;
				}
			}
			else {
				prev = current;
				current = current->next;
			}
			nextSt = current->next;
			if (nextSt == end) {
				end = current;
			}
		}
	}
	current = head;
	while (current != NULL) {
		current->id = counter;
		counter++;
		current = current->next;
	}
}

void SearchByName(student *first, char key[]) {
	bool found = false;
	for ( ; first != NULL; first = first->next) { 
		if (strstr(first->firstName, key) || strstr(first->middleName, key) || strstr(first->lastName, key)) {
			cout <<  first->id << " " << first->firstName << " " << first->middleName << " " << first->lastName << " "
				<< first->pin << endl;
			found = true;
		}
	}
	cout << endl;
	if (found == false) {
		cout << "No match found" << endl << endl;
	}
}

void SearchByPin(student *first, char key[]) {
	for( ; first != NULL; first = first->next) {
		if (strcmp(first->pin, key) == 0) {
			cout <<  first->id << " " << first->firstName << " " << first->middleName << " " << first->lastName << " "
				<< first->pin << endl << endl;
			return;
		}
	}	
	cout << endl << "No match found" << endl << endl;
}

