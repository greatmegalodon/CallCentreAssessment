"use client";

import { DefaultPagination, usePaginatedQuery } from "@/hooks/query";
import { useState } from "react";

import { Table } from "../../components/Table";
import Link from "../../components/Link";
import Pagination from "../../components/Pagination";
import { Card } from "@/components/Card";

interface User {
  id: string;
  username: string;
}

const UsersTable = () => {
  const [page, setPage] = useState(DefaultPagination.page);

  const { data, isLoading } = usePaginatedQuery<User>(
    "/User",
    { page },
    { staleTime: 0 }
  );

  const onNext = () => {
    if (page === data?.totalPages) return;

    setPage(page + 1);
  };

  const onPrevious = () => {
    if (page === 1) return;

    setPage(page - 1);
  };

  return (
    <div>
      <div className="flex mb-4">
        <Card>
          <Card.Title>{data?.totalResultsCount}</Card.Title>
          <Card.Body>Total number of Users</Card.Body>
        </Card>
      </div>
      <div className="sm:flex sm:items-center">
        <div className="sm:flex-auto">
          <h1 className="text-base font-semibold leading-6 text-white">
            Users
          </h1>
          <p className="mt-2 text-sm text-gray-300">
            A list of all the users in your account.
          </p>
        </div>
        <div className="mt-4 sm:ml-16 sm:mt-0 sm:flex-none">
          <Link href="/users/create">New User</Link>
        </div>
      </div>
      <Table.Container>
        {isLoading ? (
          <Table.Loading />
        ) : (
          <Table>
            <Table.Header>
              <Table.HeaderCell>Name</Table.HeaderCell>
              <Table.HeaderCell>
                <span className="sr-only">Edit</span>
              </Table.HeaderCell>
            </Table.Header>
            <Table.Body>
              {data?.data.map((user) => (
                <Table.Row key={user.id}>
                  <Table.Cell>{user.username}</Table.Cell>
                  <Table.Cell className="font-medium text-right">
                    <a href="#" className="text-gray-300 hover:text-white">
                      Edit<span className="sr-only">, {user.username}</span>
                    </a>
                  </Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table>
        )}
      </Table.Container>
      <Pagination
        page={page}
        totalPages={data?.totalPages}
        onNext={onNext}
        onPrevious={onPrevious}
      />
    </div>
  );
};

export default UsersTable;
