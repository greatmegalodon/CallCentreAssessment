import UsersTable from "@/app/users/table";
import { DefaultPagination, prefetchPaginatedQuery } from "@/hooks/query";
import { HydrationBoundary, dehydrate } from "@tanstack/react-query";

const UsersPage = async () => {
  const queryClient = await prefetchPaginatedQuery("/User", DefaultPagination);

  return (
    <main>
      <HydrationBoundary state={dehydrate(queryClient)}>
        <UsersTable />
      </HydrationBoundary>
    </main>
  );
};

export default UsersPage;
