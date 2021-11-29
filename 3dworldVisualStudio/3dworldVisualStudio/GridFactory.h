#pragma once
#include "Mesh.h"


namespace grid_factory
{
	class GridFactory
	{

	public:
		//float * CreateVertexAt(float* terrain, int size, int at, float valueX, float height, float valueZ);
		Mesh CreateRandomTerrain(int sizeX, int sizeZ, int mapScale);
		Mesh CreateFlatTerrain(int sizeX, int sizeZ, int mapScale);
		//Mesh InsertVertexAt(Mesh mesh, float posX, float height, float posZ);
		//float * CreateCubeOnFlatTerrain(float * terrain, int sizeX, int sizeZ, float height);


	};
}


