#include<vector>
using namespace std;

class Disjointset{
	public:
		Disjointset(int elements);
		~Disjointset() {}
		void Union(int s1, int s2);
		int Find(int element);
	private:
		vector<int> links;
		vector<int> ranks; 
};

Disjointset::Disjointset(int elements)
{
	links.resize(elements, -1);
	ranks.resize(elements, 1);
}

void Disjointset::Union(int s1, int s2)
{
	int parent;
	int child;

	if(ranks[s1] > ranks[s2])
	{
		parent = s1;
		child = s2;
	}
	else
	{
		parent = s2;
		child = s1;
	}
	links[child] = parent;
	if(ranks[parent] == ranks[child])
	{
		ranks[parent]++;
	}
}

int Disjointset::Find(int element)
{
	int parent;
	int child;

	child = -1;
	while(links[element] != -1)
	{
		parent = links[element];
		links[element] = child;
		child = element;
		element = parent;
	}
	parent = element;
	element = child;
	while(element != -1)
	{
		child = links[element];
		links[element] = parent;
		element = child;
	}
	return parent;
}

		

