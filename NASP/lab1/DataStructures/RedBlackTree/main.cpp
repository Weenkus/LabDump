// RBTreeC.cpp : Defines the entry point for the console application.
//

#include <stdio.h>
#include <stdlib.h>

#include <vector>
#include <string>
#include <fstream>
#include <iostream>

#define RED		1
#define BLACK	2

using namespace std;

void exitPrgoram();
vector<int> parseInputFile(const string& fileName);

struct node {
	int key;
	struct node *left, *right, *p;
	int color;
};

typedef struct node *NODEPTR;
struct node NIL;
NODEPTR NILPTR = &NIL;

void inorder(NODEPTR x) {
	if (x != NILPTR) {
		inorder(x->left);
		printf("%d ", x->key);
		inorder(x->right);
	}
}

NODEPTR search(NODEPTR root, int k) {
	if (root == NILPTR || root->key == k)
		return root;
	if (k < root->key)
		return search(root->left, k);
	else
		return search(root->right, k);
}

NODEPTR minimum(NODEPTR root) {
	while (root->left != NILPTR)
		root = root->left;
	return root;
}

NODEPTR maximum(NODEPTR root) {
	while (root->right != NILPTR)
		root = root->right;
	return root;
}

NODEPTR successor(NODEPTR root, int x) {
	NODEPTR temp = search(root, x);
	if (temp == NILPTR) {
		printf("%d not in tree\n", x);
		return temp;
	}
	if (temp->right != NILPTR)
		return minimum(temp->right);
	NODEPTR y = temp->p;
	while (y != NILPTR && temp == y->right) {
		temp = y;
		y = y->p;
	}
	return y;
}

NODEPTR predecessor(NODEPTR root, int x) {
	NODEPTR temp = search(root, x);
	if (temp == NILPTR) {
		printf("%d not in tree\n", x);
		return temp;
	}
	if (temp->left != NILPTR)
		return maximum(temp->left);
	NODEPTR y = temp->p;
	while (y != NILPTR && temp == y->left) {
		temp = y;
		y = y->p;
	}
	return y;
}
void leftrotate(NODEPTR *treeroot, NODEPTR x) {
	NODEPTR y = x->right;
	x->right = y->left;
	if (y->left != NILPTR)
		y->left->p = x;
	y->p = x->p;
	if (x->p == NILPTR)
		*treeroot = y;
	else if (x->p->left == x)
		x->p->left = y;
	else
		x->p->right = y;
	y->left = x;
	x->p = y;
}

void rightrotate(NODEPTR *treeroot, NODEPTR y) {
	NODEPTR x = y->left;
	y->left = x->right;
	if (x->right != NILPTR)
		x->right->p = y;
	x->p = y->p;
	if (y->p == NILPTR)
		*treeroot = x;
	else if (y->p->left == y)
		y->p->left = x;
	else
		y->p->right = x;
	x->right = y;
	y->p = x;
}

void rbinsertfixup(NODEPTR *treeroot, NODEPTR z) {
	while (z->p->color == RED) {
		if (z->p == z->p->p->left) {
			NODEPTR y = z->p->p->right;
			if (y->color == RED) {
				z->p->color = BLACK;
				y->color = BLACK;
				z->p->p->color = RED;
				z = z->p->p;
			}
			else {
				if (z == z->p->right) {
					z = z->p;
					leftrotate(treeroot, z);
				}
				z->p->color = BLACK;
				z->p->p->color = RED;
				rightrotate(treeroot, z->p->p);
			}
		}
		else {
			NODEPTR y = z->p->p->left;
			if (y->color == RED) {
				z->p->color = BLACK;
				y->color = BLACK;
				z->p->p->color = RED;
				z = z->p->p;
			}
			else {
				if (z == z->p->left) {
					z = z->p;
					rightrotate(treeroot, z);
				}
				z->p->color = BLACK;
				z->p->p->color = RED;
				leftrotate(treeroot, z->p->p);
			}
		}
	}
	(*treeroot)->color = BLACK;
}

void rbinsert(NODEPTR *treeroot, int z) {
	NODEPTR Z = (NODEPTR)malloc(sizeof(struct node));
	Z->key = z;
	NODEPTR y = NILPTR;
	NODEPTR x = *treeroot;
	while (x != NILPTR) {
		y = x;
		if (Z->key < x->key)
			x = x->left;
		else
			x = x->right;
	}
	Z->p = y;
	if (y == NILPTR)
		*treeroot = Z;
	else if (Z->key < y->key)
		y->left = Z;
	else
		y->right = Z;
	Z->left = NILPTR;
	Z->right = NILPTR;
	Z->color = RED;
	rbinsertfixup(treeroot, Z);
}

void rbtransplant(NODEPTR *treeroot, NODEPTR u, NODEPTR v) {
	if (u->p == NILPTR)
		*treeroot = v;
	else if (u == u->p->left)
		u->p->left = v;
	else
		u->p->right = v;
	v->p = u->p;
}

void rbdeletefixup(NODEPTR *treeroot, NODEPTR x) {
	while (x != *treeroot && x->color == BLACK) {
		if (x == x->p->left) {
			NODEPTR w = x->p->right;
			if (w->color == RED) {
				w->color = BLACK;
				x->p->color = RED;
				leftrotate(treeroot, x->p);
				w = x->p->right;
			}
			if (w->left->color == BLACK && w->right->color == BLACK) {
				w->color = RED;
				x = x->p;
			}
			else {
				if (w->right->color == BLACK) {
					w->left->color = BLACK;
					w->color = RED;
					rightrotate(treeroot, w);
					w = x->p->right;
				}
				w->color = x->p->color;
				x->p->color = BLACK;
				w->right->color = BLACK;
				leftrotate(treeroot, x->p);
				x = *treeroot;
			}
		}
		else {
			NODEPTR w = x->p->left;
			if (w->color == RED) {
				w->color = BLACK;
				x->p->color = RED;
				rightrotate(treeroot, x->p);
				w = x->p->left;
			}
			if (w->left->color == BLACK && w->right->color == BLACK) {
				w->color = RED;
				x = x->p;
			}
			else {
				if (w->left->color == BLACK) {
					w->right->color = BLACK;
					w->color = RED;
					leftrotate(treeroot, w);
					w = x->p->left;
				}
				w->color = x->p->color;
				x->p->color = BLACK;
				w->left->color = BLACK;
				rightrotate(treeroot, x->p);
				x = *treeroot;
			}
		}
	}
	x->color = BLACK;
}

void rbdelete(NODEPTR *treeroot, int z) {
	NODEPTR Z = search(*treeroot, z);
	if (Z == NILPTR) {
		printf("Node to be deleted not found\n");
		return;
	}
	NODEPTR y = Z;
	int yoc = y->color;
	NODEPTR x;
	if (Z->left == NILPTR) {
		x = Z->right;
		rbtransplant(treeroot, Z, Z->right);
	}
	else if (Z->right == NILPTR) {
		x = Z->left;
		rbtransplant(treeroot, Z, Z->left);
	}
	else {
		y = minimum(Z->right);
		yoc = y->color;
		x = y->right;
		if (y->p == Z)
			x->p = y;
		else {
			rbtransplant(treeroot, y, y->right);
			y->right = Z->right;
			y->right->p = y;
		}
		rbtransplant(treeroot, Z, y);
		y->left = Z->left;
		y->left->p = y;
		y->color = Z->color;
	}
	if (yoc == BLACK)
		rbdeletefixup(treeroot, x);
}

int main(int argc, char* argv[])
{
	NIL.left = NIL.right = NIL.p = NILPTR;
	NIL.color = BLACK;
	NODEPTR tree = NILPTR;
	int n;


	// Parse the input file
	vector<int> numbers = parseInputFile(argv[1]);
	for (auto num : numbers) {
		rbinsert(&tree, num);
	}
	cout << "\nStarting RedBlackTree interface...\n" << endl;

	while (1) {
		int n = 0;
		cout << "\n\n*** CHOOSE INPUT ***\n1.Insert\n2.Delete\n3.Print(inorder)\n" << endl;
		cin >> n;
		int userInput = 0;
		if (n == 1) {
			cout << "Enter any number: ";
			cin >> userInput;
			rbinsert(&tree, userInput);
		}
		else if (n == 2) {
			cout << "Enter the number to be deleted: ";
			cin >> userInput;
			rbdelete(&tree, userInput);
		}
		if (n == 1 || n == 2 || n == 3) {
			inorder(tree);
		}
	}

	exitPrgoram();
	return 0;
}

vector<int> parseInputFile(const string& fileName) {
	ifstream inputFile;
	inputFile.open(fileName);

	// Read the file
	vector<int> parsedNumbers;
	const int expectedNumberOfInputElements{ 5 };
	parsedNumbers.reserve(expectedNumberOfInputElements);


	int counter{ 0 }, fileNumber{ 0 };
	while (inputFile >> fileNumber) {
		parsedNumbers.push_back(fileNumber);
	}

	// Print what was parsed
	cout << "Parsing complet, file contained:\n";
	for (auto numb : parsedNumbers)
		cout << numb << " ";
	cout << endl;

	return parsedNumbers;
}

void exitPrgoram() {
	cout << "\nEnter any input to exit the program.";
	getchar();
}
