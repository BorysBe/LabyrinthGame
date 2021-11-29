#include "pch.h"

#include "CppUnitTest.h"
#include "../3dworldVisualStudio/GridFactory.h"
#include "../3dworldVisualStudio/GridFactory.cpp"
#include "../3dworldVisualStudio/Mesh.h"
#include "../3dworldVisualStudio/Mesh.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace grid_factory;

namespace My3dWorldTest
{
	TEST_CLASS(My3dWorldTest)
	{
	public:

		TEST_METHOD(Should_generate_random_terrain)
		{
			// Arrange
			auto gridFactory = new grid_factory::GridFactory();
			int sizeX = 2;
			int sizeZ = 2;
			int mapScale = 20;

			float expectedVertices[12] =
			{
				-10,0,10,
				10,0,10,
				-10,0,-10,
				10,0,-10
			};

			int expectedIndices[4] =
			{
				2,0,
				3,1
			};

			// Act
			auto mesh = gridFactory->CreateRandomTerrain(sizeX,sizeZ,mapScale);
			auto actualVertices = mesh.getVerticesPointer();
			auto actualIndices = mesh.getIndicesPointer();

			// Assert
			for (int i = 0; i < 4; i+=3)
			{
				Assert::AreEqual(expectedVertices[i], actualVertices[i]);
				Assert::AreNotEqual(expectedVertices[i+1], actualVertices[i+1]);
				Assert::AreEqual(expectedVertices[i+2], actualVertices[i+2]);
			}

			for (int i = 0; i < 4; i++)
			{
				Assert::AreEqual(expectedIndices[i], actualIndices[i]);
			}

		}

		TEST_METHOD(Should_generate_flat_terrain)
		{
			// Arrange
			auto gridFactory = new grid_factory::GridFactory();
			int sizeX = 2;
			int sizeZ = 2;
			int mapScale = 20;

			float expectedVertices[12] =
			{
				-10,0,10,
				10,0,10,
				-10,0,-10,
				10,0,-10
			};

			int expectedIndices[4] =
			{
				2,0,
				3,1
			};

			// Act
			auto mesh = gridFactory->CreateFlatTerrain(sizeX, sizeZ, mapScale);
			auto actualVertices = mesh.getVerticesPointer();
			auto actualIndices = mesh.getIndicesPointer();

			// Assert
			for(int i = 0; i < 12; i++)
			{
				Assert::AreEqual(expectedVertices[i],actualVertices[i]);
			}

			for (int i = 0; i < 4; i++)
			{
				Assert::AreEqual(expectedIndices[i],actualIndices[i]);
			}
		}

		// Function to insert x in arr at position pos
		int* insertX(int n, int arr[],
			int x, int pos)
		{
			int i;

			// increase the size by 1
			n++;

			// shift elements forward
			for (i = n; i >= pos; i--)
				arr[i] = arr[i - 1];

			// insert x at pos
			arr[pos - 1] = x;

			return arr;
		}

		TEST_METHOD(Should_insert_vertex_to_array)
		{
			int* arr = new int[0];
			int i, x, pos, n = 10;

			// initial array of size 10
			for (i = 0; i < 10; i++)
				arr[i] = i + 1;

			// element to be inserted
			x = 50;

			// position at which element is to be inserted
			pos = 5;

			// Insert x at pos
			insertX(n, arr, x, pos);

			std::cout << std::endl;
			insertX(n, arr, x, 2);
		}

		TEST_METHOD(Should_insert_vertex)
		{
			// Arrange
			auto gridFactory = new grid_factory::GridFactory();
			int sizeX = 2;
			int sizeZ = 2;
			int mapScale = 20;

			float expectedVertices[15] =
			{
				-10,0,10,
				10,0,10,
				-10,0,-10,
				10,0,-10,
				-5,0,10
			};

			int expectedIndices[6] =
			{
				2,0,
				4,4,
				3,1
			};

			// Act
			auto mesh = gridFactory->CreateFlatTerrain(sizeX, sizeZ, mapScale);
			//mesh = gridFactory->InsertVertexAt(mesh, -5, 0, 10);
			auto actualVertices = mesh.getVerticesPointer();
			auto actualIndices = mesh.getIndicesPointer();

			// Assert
			for (int i = 0; i < 15; i++)
			{
				Assert::AreEqual(expectedVertices[i], actualVertices[i]);
			}

			for (int i = 0; i < 6; i++)
			{
				Assert::AreEqual(expectedIndices[i], actualIndices[i]);
			}
		}

	};



	// Driver Code

}
